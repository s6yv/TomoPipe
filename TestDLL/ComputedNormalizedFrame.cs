using System;
using System.Collections.Generic;
using System.Linq;

namespace RocsoleDataConverter
{

    readonly struct RelevantCurrents
    {
        public readonly double[] inOppositeElectrodes; // electrodes 1 and 9, 2 and 10 ...
        public readonly double[] inAdjecentQuadrants; // electrodes 1 and 5, 5 and 9 ...

        public RelevantCurrents(double[] opposite, double[] adjecent)
        {
            this.inOppositeElectrodes = opposite;
            this.inAdjecentQuadrants = adjecent;
        }
    }


    readonly struct DataPoint
    {
        public readonly int id; // electrodes 1 and 9, 2 and 10 ...
        public readonly double value; // electrodes 1 and 5, 5 and 9 ...

        public DataPoint(int id, double value)
        {
            this.id = id;
            this.value = value;
        }
    }


    internal class ComputedNormalizedFrame
    {
        // not sure about the units or values. made it up :c
        readonly double maxCurrentBetweenOppositeElectrodes = 0.00099050174375;
        readonly double maxCurrentBetweenAdjecentQuadrants = 0.0014231092291666658;
        double[] allRawCurrents;
        RelevantCurrents currents;

        public ComputedNormalizedFrame(List<double> allRawCurrents)
        {
            if (allRawCurrents.Count() != 256) {
                throw new ArgumentOutOfRangeException("ComputedNormalizedFrame only supports 16 electrode setup for now (256 data points)");
            }
            this.allRawCurrents = allRawCurrents.ToArray();
            currents = Filter12RelevantMeasurements();
        }

        private double CurrentBetweenElectrodes(int firstId, int secondId){
            var id = firstId * 16 + secondId;
            return allRawCurrents[id];
        }

        private static double StandardDeviation(double[] currents) {
            double average = currents.Average();
            double sum = currents.Sum(d => Math.Pow(d - average, 2));
            return Math.Sqrt((sum) / currents.Count());
        }

        private static double NormalizeCurrent(double rawCurrent, double maxCurrent){
            return (maxCurrent - rawCurrent) / maxCurrent;
        }

        private RelevantCurrents Filter12RelevantMeasurements()
        {
            var currentInOppositeElectrodes = new double[8];
            var currentInAdjecentElectrodes = new double[4];

            for (int i = 0; i < 8; i++){
                var firstElectrodeId = i;
                var secondElectrodeId = i + 8;
                var current = CurrentBetweenElectrodes(firstElectrodeId, secondElectrodeId);
                var maxCurrent = maxCurrentBetweenOppositeElectrodes;
                currentInOppositeElectrodes[i] = NormalizeCurrent(current, maxCurrent);
            }

            for (int i = 0; i < 4; i++){
                var firstElectrodeId = i;
                var secondElectrodeId = i + 4;
                var current = CurrentBetweenElectrodes(firstElectrodeId, secondElectrodeId);
                var maxCurrent = maxCurrentBetweenAdjecentQuadrants;
                currentInAdjecentElectrodes[i] = NormalizeCurrent(current, maxCurrent);
            }


            var relevantCurrents = new RelevantCurrents(currentInOppositeElectrodes, currentInAdjecentElectrodes);
            return relevantCurrents;
        }

        public double DistanceOfGasCoreFromCentre() {
            var currentsAcross = currents.inOppositeElectrodes;
            var averageCurrent = currentsAcross.Average();
            var currentStandardDeviation = StandardDeviation(currentsAcross);

            var ratio = currentStandardDeviation / averageCurrent;
            return 6.333 * Math.Log(ratio) + 17.744;
        }

        private IOrderedEnumerable<DataPoint> LargestValues(double[] values){
            var dataPoints = new List<DataPoint>();
            for (int id = 0; id < values.Length; id++){
                var data = new DataPoint(id, values[id]);
                dataPoints.Add(data);
            }
            return dataPoints.OrderByDescending(point => point.value);
        }

        private bool IsElectrodeInQuadrant(int electrodeId, int quadrant){
            return Math.Floor((double)(electrodeId / 4)) == quadrant;
        }

        private bool IsElectrodePairInQuadrant(int pairId,  int quadrant) {
            const string invalidQuadrant = "QuadrantId is not in range <0, 3>";
            if (quadrant < 0 || quadrant > 3) throw new ArgumentOutOfRangeException(invalidQuadrant);

            var firstId = pairId;
            var secondId = pairId + 8;

            return IsElectrodeInQuadrant(firstId, quadrant) || IsElectrodeInQuadrant(secondId, quadrant);
        }


        private double ElectrodeIdClosestToGasCore(){
            var quadrantWithGasCore = LargestValues(currents.inAdjecentQuadrants).First().id;
            var mostAffectedElectrodePairId = LargestValues(currents.inOppositeElectrodes)
                .First(pair => IsElectrodePairInQuadrant(pair.id, quadrantWithGasCore)).id;

            var firstElectrodeId = mostAffectedElectrodePairId;
            var secondElectrodeId = mostAffectedElectrodePairId + 8;
            var firstInQuadrant = IsElectrodeInQuadrant(firstElectrodeId, quadrantWithGasCore);
            return firstInQuadrant ? firstElectrodeId : secondElectrodeId;
        }

        public double GasCoreOffsetAngleDeg(){
            var id = ElectrodeIdClosestToGasCore();
            return id * 22.5;
        }
    }
}

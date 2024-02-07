using System;

namespace OpenARIANA.Processor
{
    public interface IProcessor
    {
        void Process(string inputFilePath, string outputFilePath);
        void Process(string inputFilePath, string label, string outputFilePath);
    }
    public static class ProcessorFactory
    {
        public static IProcessor GetProcessor(string processorName,
            string inputFilePath, string outputFilePath)
        {
            switch (processorName)
            {
                // add future processors here
                case "Basic Processor":
                    return new BasicProcessor();
                case "itrust Processor":
                    return new ItrustProcessor();
                default:
                    throw new ArgumentException("Processor not available.");
            }
        }
    }
}

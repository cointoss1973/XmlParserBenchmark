#nullable enable
using System;
using System.IO;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

/// <summary>
/// ikorin24/U8XmlParser is licensed under the MIT License
/// https://github.com/ikorin24/U8XmlParser/blob/master/src/Benchmark/Program.cs
/// </summary>
namespace XmlParserBenchmark
{
    class Program
    {
        public static void Main()
        {
            BenchmarkRunner.Run<ParserBenchmark>();
        }
    }

    [MemoryDiagnoser]
    [MarkdownExporterAttribute.GitHub]
    //[RyuJitX64Job]
    [IterationCount(100)]
    [RankColumn]
    [SimpleJob(RuntimeMoniker.Net48, baseline: true)]
    [SimpleJob(RuntimeMoniker.Net50)]
    [SimpleJob(RuntimeMoniker.Net60)]
    public class ParserBenchmark
    {
        private readonly string _filePath;
        private Stream? _stream;


        public ParserBenchmark()
        {
            // Remove comment-out to switch the file

            //var name = "small.xml";
            var name = "large.xml";

            _filePath = Path.Combine("data/", name);
            Console.WriteLine(_filePath);
        }

        [IterationSetup]
        public void Setup()
        {
            _stream = File.OpenRead(_filePath);
        }

        [IterationCleanup]
        public void Cleanup()
        {
            _stream?.Dispose();
            _stream = null;
        }


        [Benchmark(Description = "System.Xml.Linq.XDocument")]
        public void XDocument()
        {
            var stream = _stream!;
            _ = System.Xml.Linq.XDocument.Load(stream);
        }

        [Benchmark(Description = "System.Xml.Linq.XElement")]
        public void XElement()
        {
            var stream = _stream!;
            var _ = System.Xml.Linq.XElement.Load(stream);
        }


        [Benchmark(Description = "System.Xml.XmlDocument")]
        public void XmlDocument()
        {
            var stream = _stream!;
            var xml = new System.Xml.XmlDocument();
            xml.Load(stream);
        }

        [Benchmark(Description = "System.Xml.XmlReader")]
        public void XmlReader()
        {
            var stream = _stream!;
            using var reader = System.Xml.XmlReader.Create(stream);
            while (reader.Read())
            {
            }
        }

        /// <summary>
        /// ChoXmlReader  https://www.nuget.org/packages/ChoETL.NETStandard/
        /// </summary>
        [Benchmark(Description = "ChoETL.ChoXmlReader")]
        public void ChoXmlReader()
        {
            var stream = _stream!;
            var reader = new ChoETL.ChoXmlReader(stream);
            while ((_ = reader.Read()) != null)
            {

            }
        }


        /// <summary>
        /// U8XmlParser  https://github.com/ikorin24/U8XmlParser
        /// </summary>
        //[Benchmark(Baseline = true, Description = "U8Xml.XmlParser")]
        [Benchmark(Description = "U8Xml.XmlParser")]
        public void U8XmlParser()
        {
            var stream = _stream!;
            using var xml = U8Xml.XmlParser.Parse(stream);
        }

    }
}
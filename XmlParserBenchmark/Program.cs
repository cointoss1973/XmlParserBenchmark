#nullable enable
using System;
using System.IO;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Configs;
using System.Threading.Tasks;

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
#if DEBUG
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).RunAll(new DebugInProcessConfig());
#else
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).RunAll();
#endif
        }
    }

    [MemoryDiagnoser]
    [MarkdownExporterAttribute.GitHub]
    //[RyuJitX64Job]
    [IterationCount(10)]
    [RankColumn]
    [SimpleJob(RuntimeMoniker.Net48, baseline: true)]
    [SimpleJob(RuntimeMoniker.Net50)]
    [SimpleJob(RuntimeMoniker.Net60)]
    public class ParserStreamBenchmark
    {
        private readonly Stream _stream;


        public ParserStreamBenchmark()
        {
            //var name = "small.xml";
            var name = "large.xml";

            var filePath = Path.Combine("data", name);
            var fileInfo = new FileInfo(filePath);
            Console.WriteLine($"{fileInfo} {fileInfo.Length} byte");

            using var file = File.OpenRead(filePath);
            var ms = new MemoryStream();
            file.CopyTo(ms);
            _stream = ms;
        }


        [Benchmark(Description = "System.Xml.Linq.XDocument")]
        public System.Xml.Linq.XDocument? XDocument()
        {
            _stream.Position = 0;
            var xdoc = System.Xml.Linq.XDocument.Load(_stream);
            return xdoc;
        }

        [Benchmark(Description = "System.Xml.Linq.XElement")]
        public System.Xml.Linq.XElement XElement()
        {
            _stream.Position = 0;
            var xelement = System.Xml.Linq.XElement.Load(_stream);
            return xelement;
        }


        [Benchmark(Description = "System.Xml.XmlDocument")]
        public System.Xml.XmlDocument XmlDocument()
        {
            _stream.Position = 0;
            var xmldoc = new System.Xml.XmlDocument();
            xmldoc.Load(_stream);
            return xmldoc;
        }

        [Benchmark(Description = "System.Xml.XmlReader")]
        public async Task XmlReader()
        {
            _stream.Position = 0;

            var settings = new System.Xml.XmlReaderSettings { Async = true };
            using var reader = System.Xml.XmlReader.Create(_stream, settings);
            while (await reader.ReadAsync())
            {
                switch (reader.NodeType)
                {
                    case System.Xml.XmlNodeType.Element:
                        //Console.WriteLine($"Start Element {0}", reader.Name);
                        break;
                    case System.Xml.XmlNodeType.Text:
                        //Console.WriteLine($"Text Node: {0}",
                        await reader.GetValueAsync();
                        break;
                    case System.Xml.XmlNodeType.EndElement:
                        //Console.WriteLine($"End Element {0}", reader.Name);
                        break;
                    default:
                        //Console.WriteLine($"Other node {reader.NodeType} with value {reader.Value}");
                        break;
                }
            }
        }

        ///// <summary>
        ///// ChoXmlReader  https://www.nuget.org/packages/ChoETL.NETStandard/
        ///// </summary>
        //[Benchmark(Description = "ChoETL.ChoXmlReader")]
        //public void ChoXmlReader()
        //{
        //    var stream = _stream!;
        //    var reader = new ChoETL.ChoXmlReader(stream);
        //    while ((_ = reader.Read()) != null)
        //    {

        //    }
        //}


        /// <summary>
        /// U8XmlParser  https://github.com/ikorin24/U8XmlParser
        /// </summary>
        //[Benchmark(Baseline = true, Description = "U8Xml.XmlParser")]
        [Benchmark(Description = "U8Xml.XmlParser(1.6.1)")]
        public U8Xml.XmlObject U8XmlParser()
        {
            _stream.Position = 0;
            using var xml = U8Xml.XmlParser.Parse(_stream);
            return xml;
        }

    }
}
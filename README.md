# XmlParserBenchmark

Here are the results of some benchmarks that read XML.

## Results

``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1348 (21H2)
AMD Ryzen 7 PRO 5850U with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.100
  [Host]     : .NET 6.0.0 (6.0.21.52210), X64 RyuJIT
  Job-TUSJWE : .NET 5.0.12 (5.0.1221.52207), X64 RyuJIT
  Job-MWMNMU : .NET 6.0.0 (6.0.21.52210), X64 RyuJIT
  Job-NYCYZN : .NET Framework 4.8 (4.8.4420.0), X64 RyuJIT

InvocationCount=1  IterationCount=100  UnrollFactor=1  

```
|                    Method |        Job |            Runtime |        Mean |     Error |    StdDev |      Median | Ratio | RatioSD | Rank |       Gen 0 |       Gen 1 |      Gen 2 |       Allocated |
|-------------------------- |----------- |------------------- |------------:|----------:|----------:|------------:|------:|--------:|-----:|------------:|------------:|-----------:|----------------:|
| System.Xml.Linq.XDocument | Job-TUSJWE |           .NET 5.0 |   105.89 ms |  0.486 ms |  1.424 ms |   106.04 ms |  0.90 |    0.04 |    2 |   7000.0000 |   4000.0000 |  1000.0000 |    51,898,640 B |
| System.Xml.Linq.XDocument | Job-MWMNMU |           .NET 6.0 |    97.60 ms |  2.798 ms |  8.249 ms |    91.13 ms |  0.83 |    0.08 |    1 |   7000.0000 |   4000.0000 |  1000.0000 |    51,899,328 B |
| System.Xml.Linq.XDocument | Job-NYCYZN | .NET Framework 4.8 |   117.30 ms |  1.676 ms |  4.916 ms |   119.13 ms |  1.00 |    0.00 |    3 |   8000.0000 |   3000.0000 |  1000.0000 |    52,289,032 B |
|                           |            |                    |             |           |           |             |       |         |      |             |             |            |                 |
|  System.Xml.Linq.XElement | Job-TUSJWE |           .NET 5.0 |   105.51 ms |  0.487 ms |  1.422 ms |   105.55 ms |  0.91 |    0.05 |    2 |   7000.0000 |   4000.0000 |  1000.0000 |    51,898,504 B |
|  System.Xml.Linq.XElement | Job-MWMNMU |           .NET 6.0 |    89.70 ms |  0.359 ms |  1.012 ms |    89.80 ms |  0.77 |    0.04 |    1 |   7000.0000 |   4000.0000 |  1000.0000 |    51,900,528 B |
|  System.Xml.Linq.XElement | Job-NYCYZN | .NET Framework 4.8 |   116.31 ms |  2.000 ms |  5.897 ms |   118.84 ms |  1.00 |    0.00 |    3 |   8000.0000 |   3000.0000 |  1000.0000 |    52,286,912 B |
|                           |            |                    |             |           |           |             |       |         |      |             |             |            |                 |
|    System.Xml.XmlDocument | Job-TUSJWE |           .NET 5.0 |   151.12 ms |  0.387 ms |  1.142 ms |   151.18 ms |  0.92 |    0.01 |    2 |  10000.0000 |   5000.0000 |  1000.0000 |    76,707,880 B |
|    System.Xml.XmlDocument | Job-MWMNMU |           .NET 6.0 |   131.01 ms |  2.328 ms |  6.864 ms |   134.05 ms |  0.80 |    0.04 |    1 |  10000.0000 |   5000.0000 |  1000.0000 |    76,708,584 B |
|    System.Xml.XmlDocument | Job-NYCYZN | .NET Framework 4.8 |   163.63 ms |  0.594 ms |  1.694 ms |   163.97 ms |  1.00 |    0.00 |    3 |  13000.0000 |   5000.0000 |  1000.0000 |    77,971,592 B |
|                           |            |                    |             |           |           |             |       |         |      |             |             |            |                 |
|      System.Xml.XmlReader | Job-TUSJWE |           .NET 5.0 |    29.42 ms |  0.107 ms |  0.310 ms |    29.37 ms |  0.88 |    0.01 |    2 |           - |           - |          - |       132,416 B |
|      System.Xml.XmlReader | Job-MWMNMU |           .NET 6.0 |    25.21 ms |  0.098 ms |  0.288 ms |    25.21 ms |  0.75 |    0.01 |    1 |           - |           - |          - |       132,792 B |
|      System.Xml.XmlReader | Job-NYCYZN | .NET Framework 4.8 |    33.61 ms |  0.112 ms |  0.331 ms |    33.57 ms |  1.00 |    0.00 |    3 |           - |           - |          - |       139,368 B |
|                           |            |                    |             |           |           |             |       |         |      |             |             |            |                 |
|       ChoETL.ChoXmlReader | Job-TUSJWE |           .NET 5.0 | 1,569.08 ms |  6.867 ms | 20.249 ms | 1,563.50 ms |  0.90 |    0.05 |    2 | 209000.0000 |  92000.0000 | 42000.0000 | 1,608,656,984 B |
|       ChoETL.ChoXmlReader | Job-MWMNMU |           .NET 6.0 | 1,467.02 ms |  2.187 ms |  6.131 ms | 1,465.12 ms |  0.85 |    0.04 |    1 | 209000.0000 |  92000.0000 | 39000.0000 | 1,604,642,392 B |
|       ChoETL.ChoXmlReader | Job-NYCYZN | .NET Framework 4.8 | 1,750.42 ms | 32.138 ms | 94.760 ms | 1,695.88 ms |  1.00 |    0.00 |    3 | 303000.0000 | 124000.0000 | 60000.0000 | 1,654,408,504 B |
|                           |            |                    |             |           |           |             |       |         |      |             |             |            |                 |
|           U8Xml.XmlParser | Job-TUSJWE |           .NET 5.0 |    23.28 ms |  0.116 ms |  0.341 ms |    23.28 ms |  0.59 |    0.01 |    2 |           - |           - |          - |            64 B |
|           U8Xml.XmlParser | Job-MWMNMU |           .NET 6.0 |    22.95 ms |  0.106 ms |  0.313 ms |    22.92 ms |  0.58 |    0.01 |    1 |           - |           - |          - |           544 B |
|           U8Xml.XmlParser | Job-NYCYZN | .NET Framework 4.8 |    39.77 ms |  0.182 ms |  0.538 ms |    39.78 ms |  1.00 |    0.00 |    3 |           - |           - |          - |               - |

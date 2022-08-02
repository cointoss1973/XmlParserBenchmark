# XmlParserBenchmark

Here are the results of some benchmarks that read XML.

## Results

``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1865 (21H2)
AMD Ryzen 7 PRO 5850U with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.302
  [Host]     : .NET 6.0.7 (6.0.722.32202), X64 RyuJIT
  Job-QQUJTA : .NET 5.0.17 (5.0.1722.21314), X64 RyuJIT
  Job-YFOINF : .NET 6.0.7 (6.0.722.32202), X64 RyuJIT
  Job-CYUQHH : .NET Framework 4.8 (4.8.4515.0), X64 RyuJIT

IterationCount=10  

```
|                    Method |        Job |            Runtime |      Mean |    Error |   StdDev | Ratio | RatioSD | Rank |      Gen 0 |     Gen 1 |    Gen 2 |    Allocated |
|-------------------------- |----------- |------------------- |----------:|---------:|---------:|------:|--------:|-----:|-----------:|----------:|---------:|-------------:|
| System.Xml.Linq.XDocument | Job-QQUJTA |           .NET 5.0 | 117.19 ms | 2.346 ms | 1.227 ms |  0.82 |    0.01 |    1 |  6750.0000 | 3750.0000 | 750.0000 | 51,897,772 B |
| System.Xml.Linq.XDocument | Job-YFOINF |           .NET 6.0 | 115.68 ms | 1.329 ms | 0.791 ms |  0.81 |    0.01 |    1 |  7000.0000 | 3800.0000 | 800.0000 | 51,898,758 B |
| System.Xml.Linq.XDocument | Job-CYUQHH | .NET Framework 4.8 | 142.74 ms | 2.189 ms | 1.448 ms |  1.00 |    0.00 |    2 |  9000.0000 | 3500.0000 | 750.0000 | 52,282,060 B |
|                           |            |                    |           |          |          |       |         |      |            |           |          |              |
|  System.Xml.Linq.XElement | Job-QQUJTA |           .NET 5.0 | 124.40 ms | 2.389 ms | 1.580 ms |  0.88 |    0.01 |    2 |  7000.0000 | 3800.0000 | 800.0000 | 51,897,653 B |
|  System.Xml.Linq.XElement | Job-YFOINF |           .NET 6.0 | 119.77 ms | 1.333 ms | 0.882 ms |  0.85 |    0.01 |    1 |  7000.0000 | 3800.0000 | 800.0000 | 51,899,264 B |
|  System.Xml.Linq.XElement | Job-CYUQHH | .NET Framework 4.8 | 141.62 ms | 2.999 ms | 1.983 ms |  1.00 |    0.00 |    3 |  9000.0000 | 3500.0000 | 750.0000 | 52,280,136 B |
|                           |            |                    |           |          |          |       |         |      |            |           |          |              |
|    System.Xml.XmlDocument | Job-QQUJTA |           .NET 5.0 | 175.11 ms | 3.013 ms | 1.993 ms |  0.88 |    0.02 |    1 |  9666.6667 | 5000.0000 | 666.6667 | 76,708,555 B |
|    System.Xml.XmlDocument | Job-YFOINF |           .NET 6.0 | 198.26 ms | 2.555 ms | 1.690 ms |  0.99 |    0.02 |    2 |  9750.0000 | 5250.0000 | 750.0000 | 76,708,558 B |
|    System.Xml.XmlDocument | Job-CYUQHH | .NET Framework 4.8 | 199.49 ms | 5.186 ms | 3.430 ms |  1.00 |    0.00 |    2 | 13000.0000 | 4666.6667 | 666.6667 | 77,970,379 B |
|                           |            |                    |           |          |          |       |         |      |            |           |          |              |
|      System.Xml.XmlReader | Job-QQUJTA |           .NET 5.0 |  48.33 ms | 0.324 ms | 0.214 ms |  0.80 |    0.00 |    1 |  1636.3636 |  181.8182 |        - | 13,856,799 B |
|      System.Xml.XmlReader | Job-YFOINF |           .NET 6.0 |  48.21 ms | 0.362 ms | 0.239 ms |  0.80 |    0.01 |    1 |  1636.3636 |  181.8182 |        - | 13,856,713 B |
|      System.Xml.XmlReader | Job-CYUQHH | .NET Framework 4.8 |  60.17 ms | 0.540 ms | 0.357 ms |  1.00 |    0.00 |    2 |  9444.4444 |  222.2222 |        - | 59,711,425 B |
|                           |            |                    |           |          |          |       |         |      |            |           |          |              |
|    U8Xml.XmlParser(1.1.2) | Job-QQUJTA |           .NET 5.0 |  22.22 ms | 0.285 ms | 0.188 ms |  0.69 |    0.01 |    2 |          - |         - |        - |         73 B |
|    U8Xml.XmlParser(1.1.2) | Job-YFOINF |           .NET 6.0 |  21.81 ms | 0.130 ms | 0.077 ms |  0.68 |    0.00 |    1 |          - |         - |        - |         91 B |
|    U8Xml.XmlParser(1.1.2) | Job-CYUQHH | .NET Framework 4.8 |  32.18 ms | 0.244 ms | 0.161 ms |  1.00 |    0.00 |    3 |          - |         - |        - |            - |

namespace Advent2022;

public class Day15Data
{
    public const string SAMPLE = @"Sensor at x=2, y=18: closest beacon is at x=-2, y=15
Sensor at x=9, y=16: closest beacon is at x=10, y=16
Sensor at x=13, y=2: closest beacon is at x=15, y=3
Sensor at x=12, y=14: closest beacon is at x=10, y=16
Sensor at x=10, y=20: closest beacon is at x=10, y=16
Sensor at x=14, y=17: closest beacon is at x=10, y=16
Sensor at x=8, y=7: closest beacon is at x=2, y=10
Sensor at x=2, y=0: closest beacon is at x=2, y=10
Sensor at x=0, y=11: closest beacon is at x=2, y=10
Sensor at x=20, y=14: closest beacon is at x=25, y=17
Sensor at x=17, y=20: closest beacon is at x=21, y=22
Sensor at x=16, y=7: closest beacon is at x=15, y=3
Sensor at x=14, y=3: closest beacon is at x=15, y=3
Sensor at x=20, y=1: closest beacon is at x=15, y=3";
    public const string INPUT = @"Sensor at x=545406, y=2945484: closest beacon is at x=772918, y=2626448
Sensor at x=80179, y=3385522: closest beacon is at x=772918, y=2626448
Sensor at x=2381966, y=3154542: closest beacon is at x=2475123, y=3089709
Sensor at x=2607868, y=1728571: closest beacon is at x=2715626, y=2000000
Sensor at x=746476, y=2796469: closest beacon is at x=772918, y=2626448
Sensor at x=911114, y=2487289: closest beacon is at x=772918, y=2626448
Sensor at x=2806673, y=3051666: closest beacon is at x=2475123, y=3089709
Sensor at x=1335361, y=3887240: closest beacon is at x=2505629, y=4282497
Sensor at x=2432913, y=3069935: closest beacon is at x=2475123, y=3089709
Sensor at x=1333433, y=35725: closest beacon is at x=1929144, y=529341
Sensor at x=2289207, y=1556729: closest beacon is at x=2715626, y=2000000
Sensor at x=2455525, y=3113066: closest beacon is at x=2475123, y=3089709
Sensor at x=3546858, y=3085529: closest beacon is at x=3629407, y=2984857
Sensor at x=3542939, y=2742086: closest beacon is at x=3629407, y=2984857
Sensor at x=2010918, y=2389107: closest beacon is at x=2715626, y=2000000
Sensor at x=3734968, y=3024964: closest beacon is at x=3629407, y=2984857
Sensor at x=2219206, y=337159: closest beacon is at x=1929144, y=529341
Sensor at x=1969253, y=890542: closest beacon is at x=1929144, y=529341
Sensor at x=3522991, y=3257032: closest beacon is at x=3629407, y=2984857
Sensor at x=2303155, y=3239124: closest beacon is at x=2475123, y=3089709
Sensor at x=2574308, y=111701: closest beacon is at x=1929144, y=529341
Sensor at x=14826, y=2490395: closest beacon is at x=772918, y=2626448
Sensor at x=3050752, y=2366125: closest beacon is at x=2715626, y=2000000
Sensor at x=3171811, y=2935106: closest beacon is at x=3629407, y=2984857
Sensor at x=3909938, y=1033557: closest beacon is at x=3493189, y=-546524
Sensor at x=1955751, y=452168: closest beacon is at x=1929144, y=529341
Sensor at x=2159272, y=614653: closest beacon is at x=1929144, y=529341
Sensor at x=3700981, y=2930103: closest beacon is at x=3629407, y=2984857
Sensor at x=3236266, y=3676457: closest beacon is at x=3373823, y=4223689
Sensor at x=3980003, y=3819278: closest beacon is at x=3373823, y=4223689
Sensor at x=1914391, y=723058: closest beacon is at x=1929144, y=529341
Sensor at x=474503, y=1200604: closest beacon is at x=-802154, y=776650
Sensor at x=2650714, y=3674470: closest beacon is at x=2505629, y=4282497
Sensor at x=1696740, y=586715: closest beacon is at x=1929144, y=529341
Sensor at x=3818789, y=2961752: closest beacon is at x=3629407, y=2984857";

    // NOTE: Erlang solution data - BOTH now work as an off by one error was fixed
    public const string INPUT1 = @"Sensor at x=3999724, y=2000469: closest beacon is at x=4281123, y=2282046
Sensor at x=3995530, y=8733: closest beacon is at x=3321979, y=-692911
Sensor at x=3016889, y=2550239: closest beacon is at x=2408038, y=2645605
Sensor at x=3443945, y=3604888: closest beacon is at x=3610223, y=3768674
Sensor at x=168575, y=491461: closest beacon is at x=1053731, y=-142061
Sensor at x=2820722, y=3865596: closest beacon is at x=3191440, y=3801895
Sensor at x=2329102, y=2456329: closest beacon is at x=2408038, y=2645605
Sensor at x=3889469, y=3781572: closest beacon is at x=3610223, y=3768674
Sensor at x=3256726, y=3882107: closest beacon is at x=3191440, y=3801895
Sensor at x=3729564, y=3214899: closest beacon is at x=3610223, y=3768674
Sensor at x=206718, y=2732608: closest beacon is at x=-152842, y=3117903
Sensor at x=2178192, y=2132103: closest beacon is at x=2175035, y=2000000
Sensor at x=1884402, y=214904: closest beacon is at x=1053731, y=-142061
Sensor at x=3060435, y=980430: closest beacon is at x=2175035, y=2000000
Sensor at x=3998355, y=3965954: closest beacon is at x=3610223, y=3768674
Sensor at x=3704399, y=3973731: closest beacon is at x=3610223, y=3768674
Sensor at x=1421672, y=3446889: closest beacon is at x=2408038, y=2645605
Sensor at x=3415633, y=3916020: closest beacon is at x=3191440, y=3801895
Sensor at x=2408019, y=2263990: closest beacon is at x=2408038, y=2645605
Sensor at x=3735247, y=2533767: closest beacon is at x=4281123, y=2282046
Sensor at x=1756494, y=1928662: closest beacon is at x=2175035, y=2000000
Sensor at x=780161, y=1907142: closest beacon is at x=2175035, y=2000000
Sensor at x=3036853, y=3294727: closest beacon is at x=3191440, y=3801895
Sensor at x=53246, y=3908582: closest beacon is at x=-152842, y=3117903
Sensor at x=2110517, y=2243287: closest beacon is at x=2175035, y=2000000
Sensor at x=3149491, y=3998374: closest beacon is at x=3191440, y=3801895";
}

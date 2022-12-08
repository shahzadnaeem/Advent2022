namespace Advent2022;

public class Day8Data
{
    public const string SAMPLE = @"30373
25512
65332
33549
35390";
    public const string INPUT = @"100221200032023210231132410224121421011444302143121341425413411104320412140011030313333000220101112
112202123021003133332142233323404332315445153153433313154424232233100410424422313323031220233011010
011002203101130221201112041212404433424255343232213155531144333552214422033044404110303133013102011
121111233130221221224141101130304514242212243115142242515311345141411232002244120020312220212230110
202100121002031143312404331332151115114223331225243515233534114435141142242331404201300323113201222
100021031220304112421404010433522123452131415552421124411431352333224213401342034313144213120020201
021322001301122441210433214253425221315242313124453313231211152111255512555241432402123122313012220
102112011033320244100140342511211253345112333255454336534252113251451114422242001024234213333302113
132330330131203301420044434343145455152343343332352256556523331132212421445241301141320302112302033
023322331300043142101542252542114115526343233433362336426522664522241145133152512411103311030301102
322330231444114143321233414112534365353654433452553436246452265364531225351151151443033412041221031
012321024142334102115451552132455663265336555636436352252325655253353135241221433212232340024232223
221313322104114224452245113321425622464562645423543656226366436236366342211223552335030321004420030
302122222422424154133544345544642565622466255242453626235542536222223332315231425444102032032122210
231133324324321454452534322652236666662342666356747472523623223666552524234541221123331342040102130
210020241002443524243413355654666622264244543546744763764772255422554656666314534534123124422233033
131014422414243524255521645632356643223334677577673636537354355442655654452624241512525104443021123
033403033322215332325412643445254652373556346633545346456537636346453566423552515332123444244330202
113344141003242351252543634555644673346454333353676654553676643646433222324426451413334115040003434
001230324131334423146345655352634367445777457543465476454557474433574665556354335235344443340423222
023244011152423313324656433232434376576677545673553663364375673667467762442263336432523122511002314
332301042252442131435255344627377653437674735733643774776343344336573375563254444441344341324221021
334402434231342254423323453655434536773776677775884478878536663447374436723326532641314353455441013
044342001441534222364463342375566346646375448766484784546577575636346474474644262426334344414332232
244130421523351525564645423735465337565786567845848555466788768336334436366242664256515555351430211
024000015535214533542634654746473565564746464865758448476545478886463346677543522243553255453421422
314442345152523644625564463763377468476865877455764845786446587486747367443753366233446432411453102
403000534214215322266566347455673478488876664657887477647485758778655633543776436463262445335553104
214212342251136353523565555447738584887668745867475764748577568657667634663643424344644433342131210
114243351433156335446363677757688866574765658676758768885858567668678654677476636444464535114243322
042431434431524463423673765353776887875745588995877855658794867476588746757553543264362224534141501
304145214121645333564636455447886886885877698565995687887577645687776748545456743542356254233125332
311331441555242635254434657548756467747985795655656986779866797467565645743544356362633255252311252
031543523352343526446775553558756644695895589667775569668758957957865578854634733662645643413532141
132341313125236444736663666564678758797867656955877956887969998885558485468743564462266455525122534
232352541122446346437676338577648848957677569986976865585987699675564746846753677543463556334143154
131122222556542463647436455856664477857588875699877776998998877775547558467576475473336454313145311
323351431635225653436555578777465655659599588676899686697589579979794858655753634546556432442445431
112233531246445655746654878675766685587986697767667968689785797779856485864853546544452346664152533
441132313625454663535377687586749896896879888977889996877688569867885777667647465574425232431545142
111111115654356235746577756578775857666766677988789889966967668865796774447665533366756456656544233
444252452345444655545775744655786859687696799777689978899776989678787767656644764567745346442533321
243154434235433653434536454678865969596896986887966776669777699889978977488475776367633466352332221
011224342565624633533747786446558897878876986776988967899998768656967776846856453564345652346143322
255513254542435577753688664747776969589697768898799988986867978878755798466564874553774433546435555
531145456554225737775485576687555769699797776897798787876797978897659996788865775465774226546153322
442533162233465666667386864866899875679896679977778999979877877678659766555575467467575463636255152
222435446522644555737767458468785999996898997999997899997696787896667589475664463563534454563214511
114215466262527345675785857555678986776986688778978997878888897668987577564444436473675662336222114
311512225352354555566787445776557865789678977797999877977997668696567599676867476737435246555451342
421334156234643445446758455875576958787887988878779897788997898799755999778447566575637255446655255
125142454452645575467547748465958777796899699889787977799988676968695967468678746455475566543545252
541132356636257333356646477666678579897866699899788787979798698767778866447456666337455556433533121
445221446426223553556485485665966685998799898899888979878767787768599876575858773334364454262234144
525513243446634357764748776668756995869697898999998977878876699967659655845786665456752546565235543
242413525434333433563775775447556666686688668777988879987769688978675587574767756766775543642424544
412342456656355557547778657458878557979677769688789988888698966858589758586466663545734543236112142
032124116534555566453646756448767766598786768796678968666697769566795585867844377364542222543543144
441552534565633333336768485544889775969797879987999896968699678699996564658774473347764463533212333
014334316443544233545737775568867776768987678899897898878766665677857876456548645543346266424252231
325452232353463343766655746785669595566587987667687678677799977887575876564756446554733252464333125
235313513563466536365543548586589569868987876898987896869998585688675666574767347633723365564532532
131124335362654376773466855768859778665998766696796869779797998996756554465587764356742455564311133
404343421445554355553366675767758576985659996766997797776657667759678485655666467335666422541134511
312435242152464645537634574874657856986757676888867976856999658767747446588655574536622635614544141
421213311564223563546534557645478659967967597665866686567866969995544678685456636652623544254232541
132543425342352422657766376447447764765888966699888885566685795585666784565673355564333242524212311
302331311115224263643775435787778644568655576569775967656567865844846487483657355742644444135343420
041452122556655345467337433384557668665698956966665778595596897875676866777643767426226256541552542
001153531411446425663655546568788568547498779978887586997587766584748746467645733632336523244335133
123051345234564624353634333467884847454768797578698856589577675458586647637643536245652422355524202
214113222524335633455447435736745665746584587959858876858874857758585653673436654525322545251233013
021144513155424252656646455733574856574765868885444777566868748678578533774364735645364444331342200
431211353221243564244634645643745486685475647654778847854578477454755346753436664555663251321120041
102242514555444466436542457333634684684544446466456474474878867475656557554754542332232132452524043
200242332413522436266226554463734737646765775855488765445547588777757737677755424453334532353422100
241403131545213353553445445364457564356667686685588677677558646353463653546344554664534324351240301
301440432323554425253552526545676357456386755454585758845784455753574633345364523362222213444203344
400414313234154322364222535473637766337743638757685754757674654744437433364534662421344453452322134
001422404442155511452332334335755446737463367654664454733735476754666354564255333335224221543430334
013133011342545224454646324324367366635745635763574765656353733477656734233264226223311131330044444
022111144023553523455232562425243646553465743577436756535447437664477626354554366335454123020240144
230102421342115344242436455624362236434554736674746476337464537776466226562546332255133444243414211
310212333434541233141416642226456363654747545733335637756546635676334542222542352332333111404102411
030003333221133245434443565522626652624656376645357754463575744634634423652323553532531310030201001
030023023423212441345154346256652364345653336653654434533772655254643423443531524513545021442402202
010024230302312531312212123243243535655425464532567572556636535233466523565345454255552242044131211
130202311443320415524153221422665546664426662533646433452623222644336564221342333223401212213001023
211130021411240202155121122543663636242422664454425543522663356225322252323355231113031230011332201
230111130130413041333532222115236464446226463566236266425464353655322253315233224353341443240012002
001102313010120320313214115143353526263356666453526645336434636323231453123112522432413131410213301
311322021312344404314245522512231315322442523625223426563542243513444315534543320140204003013302020
223031222001210200323443112312552151222246552363556365653546423412555511231131443113420044113212123
111320133131221133413343355424321432514225151443636656325513423253211421224222110014232232113301221
010321023321033244303433322535144241341415221344311331433254245144245522341332333121010301003132230
211110020103304321442134112151523315235121445254445414513445335245114524313310001304234213223211022
111122023302320302411300133103134411221211352515252145115412111545311213404224233340430222022031002
211120112212331032422141144010135514321521155344453422441443412441135423402430103212201303331211201
010021021121120020020341010431334133122452331123312352352313123235203300324210330421332333020210112";
}

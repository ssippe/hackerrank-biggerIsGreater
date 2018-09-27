const biggerIsGreater = require('./biggerIsGreater');

test('adds 1 + 2 to equal 3', () => {
  expect(biggerIsGreater.add(1, 2)).toBe(3);
});

test('dropChar', () => {
  expect(biggerIsGreater.dropChar("abc", 1)).toBe("ac");
  expect(biggerIsGreater.dropChar("abc", 0)).toBe("bc");
  expect(biggerIsGreater.dropChar("abc", 2)).toBe("ab");
  expect(biggerIsGreater.dropChar("a", 0)).toBe("");
});

test('getStringN', () => {
  expect(biggerIsGreater.getStringN("abc", [0, 0, 0])).toBe("abc");
  expect(biggerIsGreater.getStringN("abc", [2, 1, 0])).toBe("cba");
  expect(biggerIsGreater.getStringN("abc", [2, 0, 0])).toBe("cab");
  expect(biggerIsGreater.getStringN("abc", [2, 0, 0])).toBe("cab");  
});

test('sortStr', () => {
  expect(biggerIsGreater.sortStr("abc")).toBe("abc");
  expect(biggerIsGreater.sortStr("babc")).toBe("abbc");
});

test('getScore', () => {
  expect(biggerIsGreater.getScore("abc")).toEqual([0, 0, 0]);
  expect(biggerIsGreater.getScore("cba")).toEqual([2, 1, 0]);
  expect(biggerIsGreater.getScore("abdc")).toEqual([0, 0, 1, 0]);
  // expect(biggerIsGreater.getScore("zalqxykemvzzgaka")).toEqual([13, 0, 6, 7, 8, 8, 4, 2, 4, 4, 4, 4, 2, 0, 1, 0]);
  // expect(biggerIsGreater.getScore("zalqxykemvzzgkaa")).toEqual([13, 0, 6, 7, 8, 8, 4, 2, 4, 4, 4, 4, 2, 1, 0, 0]);
  // expect(biggerIsGreater.getScore("bbba")).toEqual([1, 1, 1, 0]);




});

test('addOne', () => {
  expect(biggerIsGreater.addOne([0, 1, 1, 0])).toEqual([0, 2, 0, 0]);
  expect(biggerIsGreater.addOne([0, 0, 0])).toEqual([0, 1, 0]);
  expect(biggerIsGreater.addOne([1, 0, 0])).toEqual([1, 1, 0]);
  expect(biggerIsGreater.addOne([1, 1, 0])).toEqual([2, 0, 0]);
  expect(biggerIsGreater.addOne([2, 1, 0])).toEqual(null);
  expect(biggerIsGreater.addOne([1, 1, 1, 0])).toEqual([1, 2, 0, 0]);
  expect(biggerIsGreater.addOne([13, 0, 6, 7, 8, 8, 4, 2, 4, 4, 4, 4, 2, 0, 1, 0])).toEqual([13, 0, 6, 7, 8, 8, 4, 2, 4, 4, 4, 4, 2, 1, 0, 0]);

});

test('biggerIsGreater', () => {
  expect(biggerIsGreater.biggerIsGreater("abc")).toEqual("acb");
  expect(biggerIsGreater.biggerIsGreater("abbb")).toEqual("babb");
  expect(biggerIsGreater.biggerIsGreater("babb")).toEqual("bbab");
  expect(biggerIsGreater.biggerIsGreater("bbab")).toEqual("bbba");
  expect(biggerIsGreater.biggerIsGreater("bbba")).toEqual("no answer");
  expect(biggerIsGreater.biggerIsGreater("acb")).toEqual("bac");
  expect(biggerIsGreater.biggerIsGreater("bb")).toEqual("no answer");
  expect(biggerIsGreater.biggerIsGreater("hefg")).toEqual("hegf");
  expect(biggerIsGreater.biggerIsGreater("dhck")).toEqual("dhkc");
  expect(biggerIsGreater.biggerIsGreater("dkhc")).toEqual("hcdk");
  expect(biggerIsGreater.biggerIsGreater("fedcbabcd")).toEqual("fedcbabdc");
  expect(biggerIsGreater.biggerIsGreater("imllmmcslslkyoegymoa")).toEqual("imllmmcslslkyoegyoam");
  expect(biggerIsGreater.biggerIsGreater("zalqxykemvzzgaka")).toEqual("zalqxykemvzzgkaa");
  expect(biggerIsGreater.biggerIsGreater("jfkaehlegohwggf")).toEqual("jfkaehlegowfggh");


});

const fullRun = (s) => {
  let res = s;
  console.log(res);
  do {    
    res = biggerIsGreater.biggerIsGreater(s);    
    console.log(res);
  } while (res !== 'no answer')
}

const biggerIsGreaterTestBulk = (input, output) => {
  const inputLines = input.split('\n');
  const ouputLines = output.split('\n');
  for (let i = 0; i < inputLines.length; i++) {
    expect(biggerIsGreater.biggerIsGreater(inputLines[i])).toBe(ouputLines[i]);
  }
}

// test('biggerIsGreaterBulk', () => {  
//   biggerIsGreaterTestBulk(`imllmmcslslkyoegymoa
//   fvincndjrurfh
//   rtglgzzqxnuflitnlyit
//   mhtvaqofxtyrz
//   zalqxykemvzzgaka
//   wjjulziszbqqdcpdnhdo
//   japjbvjlxzkgietkm
//   jqczvgqywydkunmjw
//   ehdegnmorgafrjxvksc
//   tydwixlwghlmqo
//   wddnwjneaxbwhwamr
//   pnimbesirfbivxl
//   mijamkzpiiniveik
//   qxtwpdpwexuej
//   qtcshorwyck
//   xoojiggdcyjrupr
//   vcjmvngcdyabcmjz
//   xildrrhpca
//   rrcntnbqchsfhvijh
//   kmotatmrabtcomu
//   bnfcejmyotvw
//   dnppdkpywiaxddoqx
//   tmowsxkrodmkkra
//   jfkaehlegohwggf
//   ttylsiegnttymtyx
//   kyetllczuyibdkwyihrq
//   xdhqbvlbtmmtshefjf
//   kpdpzzohihzwgdfzgb
//   kuywptftapaa
//   qfqpegznnyludrv
//   ufwogufbzaboaepslikq
//   jfejqapjvbdcxtkry
//   sypjbvatgidyxodd
//   wdpfyqjcpcn
//   baabpjckkytudr
//   uvwurzjyzbhcqmrypraq
//   kvtwtmqygksbim
//   ivsjycnooeodwpt
//   zqyxjnnitzawipqsm
//   blmrzavodtfzyepz
//   bmqlhqndacv
//   phvauobwkrcfwdecsd
//   vpygyqubqywkndhpzw
//   yikanhdrjxw
//   vnpblfxmvwkflqobrk
//   pserilwzxwyorldsxksl
//   qymbqaehnyzhfqpqprpl
//   fcakwzuqlzglnibqmkd
//   jkscckttaeifiksgkmxx
//   dkbllravwnhhfjjrce
//   imzsyrykfvjt
//   tvogoocldlukwfcajvix
//   cvnagtypozljpragvlj
//   hwcmacxvmus
//   rhrzcpprqccf
//   clppxvwtaktchqrdif
//   qwusnlldnolhq
//   yitveovrja
//   arciyxaxtvmfgquwb
//   pzbxvxdjuuvuv
//   nxfowilpdxwlpt
//   swzsaynxbytytqtq
//   qyrogefleeyt
//   iotjgthvslvmjpcchhuf
//   knfpyjtzfq
//   tmtbfayantmwk
//   asxwzygngwn
//   rmwiwrurubt
//   bhmpfwhgqfcqfldlhs
//   yhbidtewpgp
//   jwwbeuiklpodvzii
//   anjhprmkwibe
//   lpwhqaebmr
//   dunecynelymcpyonjq
//   hblfldireuivzekegit
//   uryygzpwifrricwvge
//   kzuhaysegaxtwqtvx
//   kvarmrbpoxxujhvgpw
//   hanhaggqzdpunkugzmhq
//   gnwqwsylqeuqr
//   qzkjbnyvclrkmdtc
//   argsnaqbquv
//   obbnlkoaklcx
//   ojiilqieycsasvqosycu
//   qhlgiwsmtxbffjsxt
//   vvrvnmndeogyp
//   ibeqzyeuvfzb
//   sajpyegttujxyx
//   zmdjphzogfldlkgbchnt
//   tbanvjmwirxx
//   gmdhdlmopzyvddeqyjja
//   yxvmvedubzcpd
//   soygdzhbckfuk
//   gkbekyrhcwc
//   wevzqpnqwtpfu
//   rbobquotbysufwqjeo
//   bpgqfwoyntuhkvwo
//   schtabphairewhfmp
//   rlmrahlisggguykeu
//   fjtfrmlqvsekq`,
//   `imllmmcslslkyoegyoam
//   fvincndjrurhf
//   rtglgzzqxnuflitnlyti
//   mhtvaqofxtyzr
//   zalqxykemvzzgkaa
//   wjjulziszbqqdcpdnhod
//   japjbvjlxzkgietmk
//   jqczvgqywydkunmwj
//   ehdegnmorgafrjxvsck
//   tydwixlwghlomq
//   wddnwjneaxbwhwarm
//   pnimbesirfbixlv
//   mijamkzpiiniveki
//   qxtwpdpwexuje
//   qtcshorwykc
//   xoojiggdcyjrurp
//   vcjmvngcdyabcmzj
//   xildrrpach
//   rrcntnbqchsfhvjhi
//   kmotatmrabtcoum
//   bnfcejmyotwv
//   dnppdkpywiaxddoxq
//   tmowsxkrodmkrak
//   jfkaehlegowfggh
//   ttylsiegnttymxty
//   kyetllczuyibdkwyiqhr
//   xdhqbvlbtmmtshejff
//   kpdpzzohihzwgdgbfz
//   kuywptftpaaa
//   qfqpegznnyludvr
//   ufwogufbzaboaepsliqk
//   jfejqapjvbdcxtkyr
//   sypjbvatgiodddxy
//   wdpfyqjcpnc
//   baabpjckkyturd
//   uvwurzjyzbhcqmryprqa
//   kvtwtmqygksbmi
//   ivsjycnooeodwtp
//   zqyxjnnitzawipsmq
//   blmrzavodtfzyezp
//   bmqlhqndavc
//   phvauobwkrcfwdedcs
//   vpygyqubqywkndhwpz
//   yikanhdrwjx
//   vnpblfxmvwkflqokbr
//   pserilwzxwyorldsxlks
//   qymbqaehnyzhfqpqrlpp
//   fcakwzuqlzglnidbkmq
//   jkscckttaeifiksgkxmx
//   dkbllravwnhhfjjrec
//   imzsyrykfvtj
//   tvogoocldlukwfcajvxi
//   cvnagtypozljprajglv
//   hwcmacxvsmu
//   rhrzcpprqcfc
//   clppxvwtaktchqrfdi
//   qwusnlldnolqh
//   yitverajov
//   arciyxaxtvmfgqwbu
//   pzbxvxdjuuvvu
//   nxfowilpdxwltp
//   swzsaynxbytyttqq
//   qyrogefletey
//   iotjgthvslvmjpcchufh
//   knfpyjtzqf
//   tmtbfayantwkm
//   asxwzygnngw
//   rmwiwrurutb
//   bhmpfwhgqfcqfldlsh
//   yhbidtewppg
//   jwwbeuiklpodziiv
//   anjhprmkwieb
//   lpwhqaebrm
//   dunecynelymcpyonqj
//   hblfldireuivzekegti
//   uryygzpwifrriecgvw
//   kzuhaysegaxtwqtxv
//   kvarmrbpoxxujhvgwp
//   hanhaggqzdpunkugzmqh
//   gnwqwsylqeurq
//   qzkjbnyvclrkmtcd
//   argsnaqbqvu
//   obbnlkoaklxc
//   ojiilqieycsasvqosyuc
//   qhlgiwsmtxbffjtsx
//   vvrvnmndeopgy
//   ibeqzyeuvzbf
//   sajpyegttujyxx
//   zmdjphzogfldlkgbchtn
//   tbanvjmwixrx
//   gmdhdlmopzyvddeyajjq
//   yxvmvedubzdcp
//   soygdzhbckkfu
//   gkbekyrhwcc
//   wevzqpnqwtpuf
//   rbobquotbysufwqjoe
//   bpgqfwoyntuhkwov
//   schtabphairewhfpm
//   rlmrahlisggguykue
//   fjtfrmlqvseqk`);

// });


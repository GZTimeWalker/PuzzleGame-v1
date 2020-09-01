# GZTime's Puzzle Adventure

## 一切的起源
https://t.bilibili.com/427257969311134815

## 0 start
`https://puzzle.gztime.cc`

点击 `letter`

## 1 a letter
`https://puzzle.gztime.cc/Letter`
![](https://github.com/GZTimeWalker/PuzzleGame-v1/WriteUps/images/MLj_.jpeg)
![](https://github.com/GZTimeWalker/PuzzleGame-v1/WriteUps/images/upload_e666cb8b3be7df87dae99c19afd10680.png)

邮票背景是地图，北纬 38 度，宁夏回族自治区吴忠市，邮编是 751100
`751100`

## 2 memory
`https://puzzle.gztime.cc/Memory`
Base64 解码后是 Monody - The Fat Rat 的歌词
对比原版歌词发现少字母
```
summer in the hills
those hazy days i do remember
we were running stll
had the whole world at our feet
watching seasons change
our roads were lined with adventure
ountains in the way
couldn't keep us fro the sea
here we stand pen arms
this is home where we ae
ever srong in the world that we made
i still hear you in the breeze
see your shdows in the trees
hoding on memories never change
```
![](https://github.com/GZTimeWalker/PuzzleGame-v1/WriteUps/images/upload_fb4e5f34c1792309983558c54a6ac3e0.png)


`immortal`

## 3 anime
`https://puzzle.gztime.cc/Anime`

![](https://github.com/GZTimeWalker/PuzzleGame-v1/WriteUps/images/upload_d53383477604a4f76d50aa33e532a4f1.png)

## 4 math
`https://puzzle.gztime.cc/Math`

1. sin(x)
2. -x^2+3x
3. abs(x)^3
4. tan(x^2)
5. sqrt(pi\*abs(x))

for all: sqrt(x)+sqrt(-x-1)

有bug，输入不合法的表达式直接起飞

## 5 fans
`https://puzzle.gztime.cc/Unknown/friends`
stegsolve 发现 163637592 600w
[何同学六百万合影](https://ssl-offical.720yun.com/product/static/20190416/47vkOedyzqb/1555385618.html?scene_id=52207583)

```
sk180cm     - 115 5 sh   St  
iksns       - 8   3 鹄枭 结巴 
dontshirley - 149 6 蠢C  Du 
beckyLeeh   - 78  5 爱车 Be
```
![](https://github.com/GZTimeWalker/PuzzleGame-v1/WriteUps/images/upload_8d5dc8d0a70d0f8bc800379727b9c2b3.png)

`->`代表右移

右边的id + 二进制（黑1白0）：
```
sk180cm skylinehui 3 8
iksns immmio 3 5
dontshirley dreamflyday 0 9
beckyLeeh berryswing 4 5
```

`lumodays`

## 6 excel
`https://puzzle.gztime.cc/Unknown/what`

压缩包密码 `m1RacLe`
（密码可以看图，也可以明文攻击）

![](https://github.com/GZTimeWalker/PuzzleGame-v1/WriteUps/images/upload_479baa5d3ec28e77aef7908d22433c88.png)

`3Lhy0-EA`

## 7 code
`https://puzzle.gztime.cc/Unknown/next`
动态密码
大力塞进 cyberchef magic 就行

## 8 billionaire
`https://puzzle.gztime.cc/Youth/Dingtalk`
用POST请求刷到21e

```javascript=
await (async function(){

for (;;){

await fetch("https://puzzle.gztime.cc/api/live/uploadlikes", {
    "credentials": "include",
    "headers": {
        "User-Agent": "Mozilla/5.0 (X11; Linux x86_64; rv:80.0) Gecko/20100101 Firefox/80.0",
        "Accept": "*/*",
        "Accept-Language": "en-US,en;q=0.7,zh-CN;q=0.3",
        "Content-Type": "application/x-www-form-urlencoded; charset=UTF-8",
        "X-Requested-With": "XMLHttpRequest"
    },
    "referrer": "https://puzzle.gztime.cc/Youth/Dingtalk",
    "body": "append=10000000",
    "method": "POST",
    "mode": "cors"
});

}

})()
```

## 9 trees
`https://puzzle.gztime.cc/Trees`
给出了二叉树的{前,后}序遍历和中序遍历，求另外一个就行，然后两边的答案拼起来
`TreesAreAlwaysThere.` (有句点)

## 10 emoji
`https://puzzle.gztime.cc/Through/Time`

console开冲：
```javascript=
    var host = document.location.origin;

    $.ajax({
        url: host + "/api/time",
        dataType: "json",
        type: "POST",
        headers: {
            "If-Unmodified-Since": "Thu, 27 Aug 2150 05:53:01 GMT",
        },
        success: (data) => {
            $("#0-msg").append($(data["msg"]));
            setTimeout(() => {
                $("#0-msg").fadeIn(1000);
            }, 500);
        },
    });
```

<puzzle.gztime.cc/😜>


emoji.js:
> //你听说过
> //AZTEC吗?


Aztec码
`Dreams_are_a1ways_be_with_you`

## 11 maze
`https://puzzle.gztime.cc/Maze`


dfs大法：
```python=
import requests
import sys, time, marshal

import json

sys.setrecursionlimit(10**6)

Cookie = {'token': '114514'}

x = 0
y = 0
edge = 6

directions = [
    (8, "e", 1, 0),
    (4, "w", -1, 0),
    (1, "n", 0, 1),
    (2, "s", 0, -1)
]

vis = None
try:
    vis = marshal.load(open('vis.marshal', 'rb'))
except:
    vis = [[-1 for i in range(66)] for j in range(66)]

def move(o, dx, dy):
    global x, y
    time.sleep(0.3)
    r = requests.post('https://puzzle.gztime.cc/api/maze/step', data={"drc":o}, cookies=Cookie).json()
    if r['status'] != "Success":
        print("error", x, y, o, dx, dy)
        sys.exit()
    x += dx
    y += dy
    return r['newedges']

def dfs(last, edges, depth):
    if x < 0 or x > 64 or y < 0 or y > 64:
        return
    if x == 63 and y == 63:
        print("success")
        with open("./result.txt", "w") as f:
            f.write(json.dumps(vis))
        exit(0)
    print(' '*depth, x, y, edges)
    if vis[x][y] != -1:
        return
    vis[x][y] = edges
    marshal.dump(vis, open("vis.marshal", "wb"))

    for i, tup in enumerate(directions):
        e, o, dx, dy = tup
        if i^1 == last:
            continue
        if edges & e:
            continue
        ne = move(o, dx, dy)
        dfs(i, ne, depth+1)
        _, o, dx, dy = directions[i^1]
        assert edges == move(o, dx, dy)
            
def main():
    dfs(100000, edge, 0)
    
if __name__ == '__main__':
    main()
```

9min10s通关法：
```python=
import requests, time

Cookie = {'token': 'redacted'}
path = 'eeeeeeeeneseneseeeeneseneseennnnnnwsswnwssswnnnwwsswswwwnnwsswnnneeennnwnwsswswwwneenwnwnenwwwseswwnwnnwnneesseneeeeeswsseneneennesenenneeseseeneesssesssseswwsseneseenenwwnenwwnneeeeswseeeeenwwwnnennwwswwswwnnnwnnnessennnnnneeswsssssswseeneeneneesesswssseneenneenwwwnnwneneennesessseesssseenwneesssennneennenwnwswseswwnnnenneeseennneneeswswsseswsessswwswwwseeswswssessswsswwnnnnwnnnwsswnnnwswseswwnwwnwssseeswwwwsswssenesseswwwwseeeeeneeseennwwneenwwnnnenesswseeseswsssenennessesennwneeeswseeseneseenwneeeeeeseeesennnwswnwwwnwswwneneenesseeeeeessennnnnwsswwwneenwnwswsswnnnneenwnnwswswnnnnenesseesesennenwwwnenneeenwnnnwnwwsssswwsswnwwnnwnnwswswnneneenesseenneeenwwnennnnnwwnwsswwwwneeenneeeeseseswssssesesesennwnwnnnnneenwnennwswswnwwwwnenwneennnesssswseeenwnnnenwnneeenwnwswnwswwwwseeeeswwwwsseeswwswnwwnnnenenwnwwsssswwwwsswswwseseswwwnwnnennwswwsseswsssesswsswnnwnenwnnenwnwwnnnwwnnennnwneeeseenessennesssssennneeneeeneseneenwnwswnneeneseneseesenessseneeeenwnenwwsswwnnenwwwnnwswnnwwssswwwwnnesenenwnwwnwsssswseswsswseswwwswnnnnnwssswnnnneneennnnneenwnwwswswswwsesseswwswsseesswnwwnwwswswnnneeeennenenwnwsswnnwssseswwnwnenwwswwswwnnwssswwswseeesssssenenenwwnnesenessssssssssswnwwwswnnwswswsswswnnnneneenwnwwnwswswsswwsswwnwwneenennwnnenneseswssenenennnenwwwnwswnnwwwsseswswwsseeesswnwwwssseesswsssenneeseeeeeenessseeenesswsswnnwwsessswwswwnwnnennwswsswssswwswseeeseenesennneesenneseneseenennessenesesennnnwwwnnwnenwnwwssseswsswnnnnnnneeeneseesesesessswseesesesssswwwswwswnwsseswwwswswnnennnenneeeesennwwwwnwwsswwwwnwsseswwswssesswwnwswnnnwwwwssswwwswnnwsswswnnwnwwwwwnnesenenwnnwsswnnnenwnneseennenwwnnenwwnnnwneenwnenwwnnenneeesenesesennennwwswwnnwnwwsswswnnnneenenenwwswnnnnnnwnneseeeseswsesssssssesennwnnenenwnnenesssseseeeennwswwnwneeenesesenenwnwnwwwsswwnenwnenwnwwswnnwswswnwwwwnneeseennnenessseesennnneseswsesseseeenwwnnenwwneeeseseswsseneeseneeswseesswsseeessennnwwneenennnesssenennnennwnwneeeeseswwsseswseenesseesennwnnnwwnnneeeesssssssenenwnneseseenwnwnwnneseseeesseneeesswsseessennneennennwswwwneeneneeesessswswswseeseeenennnenwneenn'

def move(o):
    time.sleep(0.27)
    r = requests.post('https://puzzle.gztime.cc/api/maze/step', data={"drc":o}, cookies=Cookie).json()

for c in path:
    move(c)
```

![](https://github.com/GZTimeWalker/PuzzleGame-v1/WriteUps/images/upload_9f299f9383ae53fbc1d6e78260166ac9.png)

## 12 NO12
`https://puzzle.gztime.cc/NO12`
DNS TXT 查询 `xn--628h.play.gztime.cc`
emoji: `<==8== [ <5j9k):n9988 ]`
rot47(-8): `4-b1c!2f1100`

Excel Sheet 2 第五行，一二两列
excel: `<====,34_zgjsjxhmf`
rot47(-5): `./Zubenescha`

Trees 关卡 html 注释
trees: `<==0b_0000_1111== |p{xNzLpG?Br`
rot47(-15): `mali?k=a803c`

三部分拼接起来

`https://puzzle.gztime.cc/Zubeneschamali?k=a803c4-b1c!2f1100`

背景SVG删遮罩图层：
![](https://github.com/GZTimeWalker/PuzzleGame-v1/WriteUps/images/upload_e37e4599cb40c1190b24a31b787420c1.png)

删去 `style="display:none;"`
第一个输入框： `not_now`
console解密：

```javascript=
var text_key = "U2FsdGVkX19KgM5OI8dZ2d8JDBwoJsxTYgzrBobHB/A6uN5KRRCI/HHwLTqSzvrvHlp7kc1pitRVCcWJ/v6iSlWkSlYlJqsAS1hMTWvsVY9zMYVd0LYI4uFKm8vKKh+L7x3d4U41oUVNFw==";
    var tmp = CryptoJS.RC4.decrypt(text_key, CryptoJS.MD5($("#key").val()).toString());
    try {
        console.log(CryptoJS.enc.Utf8.stringify(tmp));
    } catch (error) {
        console.log("wrong key");
    }
```

得到：
> FIND ?: I sit at my window this morning where (?) stops for a moment, nods to me and goes.

出自飞鸟集

> I sit at my window this morning where **the world like a passer-by** stops for a moment, nods to me and goes.

第二个输入框： `the world like a passer-by`
console提交：

```javascript=
updateProcess("Ending?", CryptoJS.MD5($("#key").val() + $("#key_text").val()).toString(), true, () => {
        $("#c-msg").fadeIn(1000);
    });
```

## 13 终点
`https://puzzle.gztime.cc/GZTime`
> 请将《三体3》的最后一句话以及你的Token发给我

> 在一小块陆地上的草丛中，有一滴露珠从一片草叶上脱离，旋转着飘起，向太空中折射出一缕晶莹的阳光。

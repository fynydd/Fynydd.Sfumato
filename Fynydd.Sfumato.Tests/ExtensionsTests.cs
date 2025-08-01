namespace Fynydd.Sfumato.Tests;

public class ExtensionsTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    #region Constants

    public static string Markup => """
                                    <!DOCTYPE html>
                                    <html lang="en" class="font-sans">
                                    <head>
                                        <meta charset="UTF-8">
                                        <title>Sample Website</title>
                                        <meta name="viewport" content="width=device-width, initial-scale=1">
                                        <link rel="stylesheet" href="css/sfumato.css">
                                    </head>
                                    <body class="phablet:hover:text-xs theme-midnight:text-lime-950 dark:text-base/5 xl:text-base/[3rem] [-webkit-backdrop-filter:blur(1rem)]">
                                        <div id="test-home" class="text-[1rem] lg:text-[1.25rem] xl:text-(length:--my-text-size) bg-fuchsia-500 dark:sm:bg-fuchsia-300 dark:text-[length:1rem] xl:text-[#112233] xl:text-[red] xl:text-[--my-color-var] xl:text-[var(--my-color-var)]">
                                            <p class="[font-weight:900] sm:[font-weight:900]">Placeholder</p>
                                            <p class="[fontweight:400] sm:[fontweight:300] xl:text[#112233] xl:text-slate[#112233] xl:text-slate-50[#112233] xxl:text-slate-50-[#112233]">Invalid Classes</p>
                                        </div>
                                        <div class="content-['Hello!'] [--margin-val6:_1.25rem]! dark:sm:supports-backdrop-blur:motion-safe:block invisible lg:max-xl:top-8 break-after-auto container aspect-screen xxl:aspect-[8/4]"></div>
                                        <div class="-top-px"></div>
                                        <div class="top-1/2 antialiased"></div>
                                        <script>
                                            function test() {
                                              let el = document.getElementById('test-element');
                                              if (el) {
                                                    el.classList.add($`
                                                        bg-emerald-900
                                                        [font-weight:700]
                                                        md:[font-weight:700]
                                                    `);
                                                    el.classList.add(`bg-emerald-950`);
                                                    el.classList.add(`[font-weight:600]`);
                                                    el.classList.add(`lg:[font-weight:600]`);
                                              }
                                            }
                                        </script>
                                        @{
                                            var test1 = $""
                                                block bg-slate-400
                                            "";
                                            
                                            var detailsMask = $"<span class=\"line-clamp-1 -mt-1! text-slate-500 dark:text-dark-foreground-dim line-clamp-2\"><span class=""line-clamp-2"">{description}</span></span>";
                                        }
                                        <p>top-999</p>
                                    </body>
                                    </html>
                                    """;
    
    private static string Css => """
                                    /* SFUMATO BROWSER RESET 
                                        /* Nested comment */
                                    */
                                    *,
                                    ::before,
                                    ::after {
                                      box-sizing: border-box;
                                      border-width: 0;
                                      border-style: solid;
                                      border-color: transparent;
                                    }
                                    
                                    * {
                                      min-width: 0px;
                                      min-height: 0rem;
                                    }
                                    
                                    div#main {
                                        margin-top: var(--top-margin);
                                    }
                                    
                                    html {
                                      line-height: 1.5;
                                      -webkit-text-size-adjust: 100%;
                                      -moz-tab-size: 4;
                                      tab-size: 4;
                                      font-family: ui-sans-serif, system-ui, -apple-system, BlinkMacSystemFont, "Aptos", "Segoe UI", Roboto, "Helvetica Neue", Arial, "Noto Sans", sans-serif, "Apple Color Emoji", "Segoe UI Emoji", "Segoe UI Symbol", "Noto Color Emoji";
                                    }
                                    
                                    /*# sourceMappingURL=app.css.map */
                                    
                                    """;

    private static string Minified => """
                                      *,::before,::after{box-sizing:border-box;border-width:0;border-style:solid;border-color:transparent}*{min-width:0;min-height:0}div#main{margin-top:var(--top-margin)}html{line-height:1.5;-webkit-text-size-adjust:100%;-moz-tab-size:4;tab-size:4;font-family:ui-sans-serif,system-ui,-apple-system,BlinkMacSystemFont,"Aptos","Segoe UI",Roboto,"Helvetica Neue",Arial,"Noto Sans",sans-serif,"Apple Color Emoji","Segoe UI Emoji","Segoe UI Symbol","Noto Color Emoji"}
                                      """;

    #endregion

    // ConsolidateAtSymbols(source, out var result)
    
    [Fact]
    public void ConsolidateAtSymbolsTest()
    {
        Assert.Equal("container", "container".ConsolidateAtSymbols());
        Assert.Equal("@container", "@container".ConsolidateAtSymbols());
        Assert.Equal("@sm:text-base", "@sm:text-base".ConsolidateAtSymbols());
        Assert.Equal("@sm:@max-lg:text-base", "@sm:@max-lg:text-base".ConsolidateAtSymbols());
        
        Assert.Equal("@container", "@@container".ConsolidateAtSymbols());
        Assert.Equal("@sm:text-base", "@@sm:text-base".ConsolidateAtSymbols());
        Assert.Equal("@sm:@max-lg:text-base", "@@sm:@@max-lg:text-base".ConsolidateAtSymbols());
    }
    
    [Fact]
    public void StringFnv1A()
    {
        Assert.Equal(16953580889345953351, "@media screen (min-width: 40rem) and (max-width: 80rem) {".Fnv1AHash64());
        Assert.Equal(14858257461132010124, "@media screen (min-width: 40rem) and (max-width: 81rem) {".Fnv1AHash64());
        Assert.Equal(11325783342227245128, "@media screen (min-width: 40rem) {".Fnv1AHash64());
    }

    [Fact]
    public void StringBuilderFnv1A()
    {
        Assert.Equal(16953580889345953351, new StringBuilder("@media screen (min-width: 40rem) and (max-width: 80rem) {").Fnv1AHash64());
        Assert.Equal(14858257461132010124, new StringBuilder("@media screen (min-width: 40rem) and (max-width: 81rem) {").Fnv1AHash64());
        Assert.Equal(11325783342227245128, new StringBuilder("@media screen (min-width: 40rem) {").Fnv1AHash64());
    }
    
    [Fact]
    public void CompactCss()
    {
        Assert.Equal(Minified, Css.CompactCss());
    }

    [Fact]
    public void IdentifyColor()
    {
        Assert.True("rgba(10,20,30,1.0)".IsValidWebColor());
        Assert.True("rgb(10,20,30)".IsValidWebColor());

        Assert.True("#aabbcc".IsValidWebColor());
        Assert.True("#aab".IsValidWebColor());
        Assert.True("#abcd".IsValidWebColor());

        Assert.True("aquamarine".IsValidWebColor());

        Assert.True("oklch(0.704 0.191 22.216)".IsValidWebColor());
        Assert.True("oklch(0.704 0.191 22.216 / 0.5)".IsValidWebColor());
    }

    [Fact]
    public void SetWebColorAlpha()
    {
        Assert.Equal("rgba(10,20,30,0.5)", "rgba(10,20,30,1.0)".SetWebColorAlpha(50));
        Assert.Equal("rgba(10,20,30,0.5)", "rgba(10,20,30,1.0)".SetWebColorAlpha(0.5));
        Assert.Equal("rgba(10,20,30,0.25)", "rgba(10,20,30,0.5)".SetWebColorAlpha(25));
        Assert.Equal("rgb(10,20,30)", "rgba(10, 20, 30, 0.5)".SetWebColorAlpha(100));

        Assert.Equal("rgba(170,187,204,0.5)", "#aabbcc".SetWebColorAlpha(0.5));
        Assert.Equal("rgba(170,170,187,0.5)", "#aab".SetWebColorAlpha(0.5));
        Assert.Equal("rgba(170,187,204,0.5)", "#abcd".SetWebColorAlpha(0.5));

        Assert.Equal("rgba(127,255,212,0.5)", "aquamarine".SetWebColorAlpha(0.5));

        Assert.Equal("oklch(0.704 0.191 22.216)", "oklch(0.704 0.191 22.216)".SetWebColorAlpha());
        Assert.Equal("oklch(0.704 0.191 22.216 / 0.5)", "oklch(0.704 0.191 22.216)".SetWebColorAlpha(50));
        Assert.Equal("oklch(0.704 0.191 22.216 / 0.5)", "oklch(0.704 0.191 22.216)".SetWebColorAlpha(0.5));
        Assert.Equal("oklch(0.704 0.191 22.216 / 0.25)", "oklch(0.704 0.191 22.216 / 0.5)".SetWebColorAlpha(0.25));
        Assert.Equal("oklch(0.704 0.191 22.216 / 0.25)", "oklch(0.704 0.191 22.216/0.5)".SetWebColorAlpha(0.25));
        Assert.Equal("oklch(0.704 0.191 22.216 / 0.5)", "oklch(0.704 0.191 22.216 / 0.5)".SetWebColorAlpha());
        Assert.Equal("oklch(0.704 0.191 22.216)", "oklch(0.704 0.191 22.216 / 0.5)".SetWebColorAlpha(1.0d));
        Assert.Equal("oklch(0.704 0.191 22.216)", "oklch(0.704 0.191 22.216 / 0.5)".SetWebColorAlpha(100));
    }
}

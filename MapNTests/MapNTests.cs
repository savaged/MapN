namespace MapNTests;

public class MapperExTests
{
    private readonly ExampleModel _eg;

    public MapperExTests()
    {
        _eg = new ExampleModel
        {
            PropOne = "Prop 1",
            PropTwo = 2,
            PropThree = DateTime.MaxValue,
            PropFour = true
        };
    }

    [Fact]
    public void TestToClone()
    {
        var clone = _eg.ToClone();
        Assert.NotNull(clone);
        Assert.IsType<ExampleModel>(clone);
        Assert.NotEqual(_eg, clone);
        Assert.NotSame(_eg, clone);
        Assert.Equal(_eg.PropOne, clone.PropOne);
        Assert.Equal(_eg.PropTwo, clone.PropTwo);
        Assert.Equal(_eg.PropThree, clone.PropThree);
        Assert.Equal(_eg.PropFour, clone.PropFour);
        Assert.Equal(_eg.PropFive, clone.PropFive);
    }

    [Fact]
    public void TestToMerged()
    {
        var another = new ExampleModel
        {
            PropOne = "Property One",
            PropThree = DateTime.MinValue,
            PropFive = "Prop 5"
        };
        var merged = _eg.ToMerged(another);
        Assert.NotNull(merged);
        Assert.NotSame(_eg, merged);
        Assert.NotEqual(_eg, merged);
        Assert.Equal(another.PropOne, merged.PropOne);
        Assert.Equal(another.PropTwo, merged.PropTwo);
        Assert.Equal(another.PropThree, merged.PropThree);
        Assert.Equal(another.PropFour, merged.PropFour);
        Assert.Equal(another.PropFive, merged.PropFive);
        Assert.NotEqual(_eg.PropFive, merged.PropFive);
    }

    [Fact]
    public void TestToOther()
    {
        var dto = _eg.ToOther<ExampleModel, ExampleDTO>();
        Assert.NotNull(dto);
        Assert.Equal(_eg.PropOne, dto.PropOne);
        Assert.Equal(_eg.PropTwo, dto.PropTwo);
        Assert.Equal(_eg.PropThree, dto.PropThree);
        Assert.Equal(_eg.PropFive, dto.PropFive);
        Assert.Equal(0, dto.PropSix);
    }

}

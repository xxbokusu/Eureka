using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class NewTestScript {

    [Test]
    public void ExampleTest()
    {
        var a = 10;
        var b = 10;
        Assert.That(a == b);
    }

    [UnityTest]
    public IEnumerator ExampleTestEnumerator()
    {
        Assert.That(1 < 10);
        yield return null;
        Assert.That(2 < 10);
        yield return null;
        Assert.That(3 < 10);
    }

}

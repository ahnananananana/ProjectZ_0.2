using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HDV;

public class TestViewModel : UIViewModel<TestModel>
{
    private EventString testName = new EventString();

    public EventString TestName => testName;

    protected override void OnBindModel(TestModel model)
    {
        model.testName.ChangeEvent += testName.SetValue;
        testName.Value = model.testName.Value;
    }
}

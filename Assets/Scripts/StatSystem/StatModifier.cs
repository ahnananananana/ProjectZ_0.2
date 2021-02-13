
namespace HDV
{
    public class StatModifier
    {
        public Operation Operation { get; private set; }
        public float Value { get; private set; }
        public object Source { get; private set; }

        public StatModifier(Operation operation, float value, object source = null)
        {
            Operation = operation;
            Value = value;
            Source = source;
        }
    }
}

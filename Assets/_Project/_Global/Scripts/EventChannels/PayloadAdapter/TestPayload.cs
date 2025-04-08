namespace _Global.EventChannels.PayloadAdapter {
    // If you change the name of the payload class, make sure to update the name of the adapter class as well.
    public class TestPayload {
        public int TestInt;
        public string TestString;
        public float TestFloat;
        public double TestDouble;
        public int TestInt2;
        
        public TestPayload(int testInt, string testString, float testFloat, double testDouble, int testInt2) {
            TestInt = testInt;
            TestString = testString;
            TestFloat = testFloat;
            TestDouble = testDouble;
            TestInt2 = testInt2;
        }
    }
}
using Xunit.Abstractions;
using Xunit.Sdk;

namespace OrderManagementAPI.Integration.Test
{
    // https://learn.microsoft.com/en-us/dotnet/core/testing/order-unit-tests?pivots=xunit
    public class PriorityOrderer : ITestCaseOrderer
    {
        public const string TypeName = "OrderManagementAPI.Integration.Test.PriorityOrderer";

        public const string AssemblyName = "OrderManagementAPI.Integration.Test";
        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(
            IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
        {
            string assemblyName = typeof(TestPriorityAttribute).AssemblyQualifiedName!;
            var sortedMethods = new SortedDictionary<int, List<TTestCase>>();
            foreach (TTestCase testCase in testCases)
            {
                int priority = testCase.TestMethod.Method
                    .GetCustomAttributes(assemblyName)
                    .FirstOrDefault()
                    ?.GetNamedArgument<int>(nameof(TestPriorityAttribute.Priority)) ?? 0;

                GetOrCreate(sortedMethods, priority).Add(testCase);
            }

            foreach (TTestCase testCase in
                sortedMethods.Keys.SelectMany(
                    priority => sortedMethods[priority].OrderBy(
                        testCase => testCase.TestMethod.Method.Name)))
            {
                yield return testCase;
            }
        }

        private static TValue GetOrCreate<TKey, TValue>(
            IDictionary<TKey, TValue> dictionary, TKey key)
            where TKey : struct
            where TValue : new() =>
            dictionary.TryGetValue(key, out TValue? result)
                ? result
                : (dictionary[key] = new TValue());
    }
}

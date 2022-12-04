﻿using AnyDiff.Tests.TestObjects;
using AnyDiff.Tests.TestObjects.Complex;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using GoLive.AnyDiff;

namespace AnyDiff.Tests
{
    [TestFixture]
    public class CollectionTests
    {
        [Test]
        public void ShouldDetect_Array_Differences()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { IntArray = new int[] { 0, 0, 5, 0, 0 } };
            var obj2 = new TestObject { IntArray = new int[] { 0, 0, 2, 0, 0 } };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
            Assert.AreEqual(-3, diff.First().Delta);
            Assert.AreEqual(2, diff.First().ArrayIndex);
        }

        [Test]
        public void ShouldDetect_Array_NoDifferences()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { IntArray = new int[] { 0, 0, 5, 0, 0 } };
            var obj2 = new TestObject { IntArray = new int[] { 0, 0, 5, 0, 0 } };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldDetect_List_Differences()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { IntCollection = new List<int> { 0, 0, 5, 0, 0 } };
            var obj2 = new TestObject { IntCollection = new List<int> { 0, 0, 2, 0, 0 } };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
            Assert.AreEqual(-3, diff.First().Delta);
            Assert.AreEqual(2, diff.First().ArrayIndex);
        }

        [Test]
        public void ShouldDetect_List_CountDifferences()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { IntCollection = new List<int> { 0, 0, 5, 0, 0, 0 } };
            var obj2 = new TestObject { IntCollection = new List<int> { 0, 0, 5, 0, 0 } };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
        }

        [Test]
        public void ShouldDetect_List_CountDifferences2()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { IntCollection = new List<int> { 0, 0, 5, 0, 0 } };
            var obj2 = new TestObject { IntCollection = new List<int> { 0, 0, 5, 0, 0, 0 } };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
        }

        [Test]
        public void ShouldBeEqual_EmptyList_Null()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { IntCollection = new List<int> { } };
            var obj2 = new TestObject { IntCollection = null };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2, ComparisonOptions.All | ComparisonOptions.TreatEmptyListAndNullTheSame);
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldNotBeEqual_EmptyList_Null()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { IntCollection = new List<int> { } };
            var obj2 = new TestObject { IntCollection = null };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2, ComparisonOptions.All);
            Assert.AreEqual(1, diff.Count);
        }

        [Test]
        public void ShouldDetect_List_NoDifferences()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { IntCollection = new List<int> { 0, 0, 5, 0, 0 } };
            var obj2 = new TestObject { IntCollection = new List<int> { 0, 0, 5, 0, 0 } };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldDetect_Dictionary_Differences()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { IntDictionary = new Dictionary<int, string> { { 0, "1" }, { 1, "2" }, { 2, "3" }, { 3, "4" }, { 4, "5" } } };
            var obj2 = new TestObject { IntDictionary = new Dictionary<int, string> { { 0, "1" }, { 1, "2" }, { 2, "2" }, { 3, "4" }, { 4, "5" } } };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(2, diff.Count);
        }

        [Test]
        public void ShouldDetect_Dictionary_NoDifferences()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { IntDictionary = new Dictionary<int, string> { { 0, "1" }, { 1, "2" }, { 2, "3" }, { 3, "4" }, { 4, "5" } } };
            var obj2 = new TestObject { IntDictionary = new Dictionary<int, string> { { 0, "1" }, { 1, "2" }, { 2, "3" }, { 3, "4" }, { 4, "5" } } };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldDetect_Dictionary_CountDifferences()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { IntDictionary = new Dictionary<int, string> { { 0, "1" }, { 1, "2" }, { 2, "3" }, { 3, "4" }, { 4, "5" }, {5, "6" } } };
            var obj2 = new TestObject { IntDictionary = new Dictionary<int, string> { { 0, "1" }, { 1, "2" }, { 2, "3" }, { 3, "4" }, { 4, "5" } } };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
        }

        [Test]
        public void ShouldDetect_Dictionary_CountDifferences2()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { IntDictionary = new Dictionary<int, string> { { 0, "1" }, { 1, "2" }, { 2, "3" }, { 3, "4" }, { 4, "5" } } };
            var obj2 = new TestObject { IntDictionary = new Dictionary<int, string> { { 0, "1" }, { 1, "2" }, { 2, "3" }, { 3, "4" }, { 4, "5" }, { 5, "6" } } };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
        }

        [Test]
        public void ShouldDetect_Hashtable_NoDifferences()
        {
            var provider = new DiffProvider();
            var obj1 = new System.Collections.Hashtable();
            obj1.Add(1, "Test");
            obj1.Add(2, "Test");
            var obj2 = new System.Collections.Hashtable();
            obj2.Add(1, "Test");
            obj2.Add(2, "Test");

            var diff = provider.ComputeDiff(obj1, obj2);
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldDetect_Hashtable_CountDifferences()
        {
            var provider = new DiffProvider();
            var obj1 = new System.Collections.Hashtable();
            obj1.Add(1, "Test");
            obj1.Add(2, "Test");
            var obj2 = new System.Collections.Hashtable();
            obj2.Add(1, "Test");
            obj2.Add(2, "Test");
            obj2.Add(3, "Test");

            var diff = provider.ComputeDiff(obj1, obj2);
            Assert.Greater(diff.Count, 0);
        }

        [Test]
        public void ShouldDetect_Hashtable_CountDifferences2()
        {
            var provider = new DiffProvider();
            var obj1 = new System.Collections.Hashtable();
            obj1.Add(1, "Test");
            obj1.Add(2, "Test");
            obj1.Add(3, "Test");
            var obj2 = new System.Collections.Hashtable();
            obj2.Add(1, "Test");
            obj2.Add(2, "Test");

            var diff = provider.ComputeDiff(obj1, obj2);
            Assert.Greater(diff.Count, 0);
        }

        [Test]
        public void ShouldDetect_Hashtable_Differences()
        {
            var provider = new DiffProvider();
            var obj1 = new System.Collections.Hashtable();
            obj1.Add(1, "Test");
            obj1.Add(3, "Test");
            var obj2 = new System.Collections.Hashtable();
            obj2.Add(1, "Test");
            obj2.Add(2, "Test");

            var diff = provider.ComputeDiff(obj1, obj2, ComparisonOptions.All, "._keys", "._buckets");
            Assert.Greater(diff.Count, 0);
        }

        [Test]
        public void ShouldDetect_Array_OutOfOrder_NoDifferences()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { IntArray = new int[] { 1, 2, 3, 4, 5 } };
            var obj2 = new TestObject { IntArray = new int[] { 2, 5, 4, 3, 1 } };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2, ComparisonOptions.All | ComparisonOptions.AllowCollectionsToBeOutOfOrder);
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldDetect_Array_OutOfOrder_LeftDifferences()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { IntArray = new int[] { 1, 2, 3, 4, 6 } };
            var obj2 = new TestObject { IntArray = new int[] { 2, 5, 4, 3, 1 } };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2, ComparisonOptions.All | ComparisonOptions.AllowCollectionsToBeOutOfOrder);
            Assert.AreEqual(2, diff.Count);
        }

        [Test]
        public void ShouldDetect_Array_OutOfOrder_RightDifferences()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { IntArray = new int[] { 1, 2, 3, 4, 5 } };
            var obj2 = new TestObject { IntArray = new int[] { 2, 5, 4, 3, 6 } };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2, ComparisonOptions.All | ComparisonOptions.AllowCollectionsToBeOutOfOrder);
            Assert.AreEqual(2, diff.Count);
        }

        [Test]
        public void ShouldDetect_Array_OutOfOrder_LeftAdditionalElements()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { IntArray = new int[] { 1, 2, 3, 4, 5, 9 } };
            var obj2 = new TestObject { IntArray = new int[] { 2, 5, 4, 3, 1 } };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2, ComparisonOptions.All | ComparisonOptions.AllowCollectionsToBeOutOfOrder);
            Assert.AreEqual(1, diff.Count);
        }

        [Test]
        public void ShouldDetect_Array_OutOfOrder_RightAdditionalElements()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { IntArray = new int[] { 1, 2, 3, 4, 5 } };
            var obj2 = new TestObject { IntArray = new int[] { 2, 5, 4, 3, 1, 8 } };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2, ComparisonOptions.All | ComparisonOptions.AllowCollectionsToBeOutOfOrder);
            Assert.AreEqual(1, diff.Count);
        }

        [Test]
        public void ShouldDetect_Array_OutOfOrder_DifferentCount()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { IntArray = new int[] { 1, 2, 3, 4, 5 } };
            var obj2 = new TestObject { IntArray = new int[] { 2, 5, 4, 3, 1, 3 } };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2, ComparisonOptions.All | ComparisonOptions.AllowCollectionsToBeOutOfOrder);
            Assert.AreEqual(1, diff.Count);
        }

        [Test]
        public void ShouldDetect_ReadOnlyCollection_NoDifferences()
        {
            var provider = new DiffProvider();
            var list = new List<EqualsObject> {
                new EqualsObject(1, "Testing 1"),
                new EqualsObject(2, "Testing 2"),
            };
            var list2 = new List<EqualsObject> {
                new EqualsObject(2, "Testing 2"),
                new EqualsObject(1, "Testing 1"),
            };
            var obj1 = new ReadOnlyCollectionObject(list);
            var obj2 = new ReadOnlyCollectionObject(list2);
            var diff = provider.ComputeDiff<ReadOnlyCollectionObject>(obj1, obj2, ComparisonOptions.All | ComparisonOptions.IncludeList | ComparisonOptions.AllowCollectionsToBeOutOfOrder | ComparisonOptions.AllowEqualsOverride,
                    x => x.Collection);

            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldDetect_ReadOnlyCollection_CustomEquals_AreDifferent()
        {
            var provider = new DiffProvider();
            var list = new List<EqualsObject> {
                new EqualsObject(1, "Testing 1"),
                new EqualsObject(2, "Testing 2"),
            };
            var list2 = new List<EqualsObject> {
                new EqualsObject(1, "Testing 1"),
                new EqualsObject(3, "This line is entirely different"),
            };
            var obj1 = new ReadOnlyCollectionObject(list);
            var obj2 = new ReadOnlyCollectionObject(list2);
            var diff = provider.ComputeDiff<ReadOnlyCollectionObject>(obj1, obj2, ComparisonOptions.All | ComparisonOptions.IncludeList | ComparisonOptions.AllowCollectionsToBeOutOfOrder | ComparisonOptions.AllowEqualsOverride,
                    x => x.Collection);

            Assert.Greater(diff.Count, 0);
        }

        [Test]
        public void ShouldDetect_ReadOnlyCollection_AreDifferent()
        {
            var provider = new DiffProvider();
            var list = new List<EqualsObject> {
                new EqualsObject(1, "Testing 1"),
                new EqualsObject(2, "Testing 2"),
            };
            var list2 = new List<EqualsObject> {
                new EqualsObject(1, "Testing 1"),
                new EqualsObject(3, "Testing 3"),
            };
            var obj1 = new ReadOnlyCollectionObject(list);
            var obj2 = new ReadOnlyCollectionObject(list2);
            var diff = provider.ComputeDiff<ReadOnlyCollectionObject>(obj1, obj2, ComparisonOptions.All | ComparisonOptions.IncludeList | ComparisonOptions.AllowEqualsOverride,
                    x => x.Collection);

            Assert.Greater(diff.Count, 0);
        }

        [Test]
        public void ShouldDetect_ReadOnlyCollection_OutOfOrder_AreDifferent()
        {
            var provider = new DiffProvider();
            var list = new List<EqualsObject> {
                new EqualsObject(1, "Testing 1"),
                new EqualsObject(2, "Testing 2"),
            };
            var list2 = new List<EqualsObject> {
                new EqualsObject(1, "Testing 1"),
                new EqualsObject(3, "Testing 3"),
            };
            var obj1 = new ReadOnlyCollectionObject(list);
            var obj2 = new ReadOnlyCollectionObject(list2);
            var diff = provider.ComputeDiff<ReadOnlyCollectionObject>(obj1, obj2, ComparisonOptions.All | ComparisonOptions.IncludeList | ComparisonOptions.AllowCollectionsToBeOutOfOrder | ComparisonOptions.AllowEqualsOverride,
                    x => x.Collection);

            Assert.Greater(diff.Count, 0);
        }

        [Test]
        public void ShouldDetect_ComplexJson_AllowOutOfOrder_NoDifferences()
        {
            var provider = new DiffProvider();

            var left = JsonConvert.DeserializeObject<ComplexDataSpec>(File.ReadAllText(GetFullPath(@"Data\ComplexJson1.json")));
            var right = JsonConvert.DeserializeObject<ComplexDataSpec>(File.ReadAllText(GetFullPath(@"Data\ComplexJson2.json")));

            var options = ComparisonOptions.All | ComparisonOptions.AllowCollectionsToBeOutOfOrder | ComparisonOptions.AllowEqualsOverride;

            var diff = provider.ComputeDiff<ComplexDataSpec>(left, right, options,
                x => x.DateCreatedUtc,
                x => x.DateModifiedUtc,
                x => x.ChangesetItemHash,
                x => x.Id,
                x => x.Rules.Select(y => y.Id),
                x => x.DayParts.Select(y => y.BuySpecificationDayPartId),
                x => x.DayParts.Select(y => y.Budgets.Select(z => z.Id)),
                x => x.DayParts.Select(y => y.Rules.Select(z => z.Id)),
                // TODO: temp leave these ignores till Name and Code are removed from DspDayPart api model
                x => x.DayParts.Select(y => y.Name),
                x => x.DayParts.Select(y => y.Code));

            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldDetect_ComplexJson_OutOfOrder_HasDifferences()
        {
            var provider = new DiffProvider();

            var left = JsonConvert.DeserializeObject<ComplexDataSpec>(File.ReadAllText(GetFullPath(@"Data\ComplexJson1.json")));
            var right = JsonConvert.DeserializeObject<ComplexDataSpec>(File.ReadAllText(GetFullPath(@"Data\ComplexJson2-OutOfOrderDemoId.json")));

            var options = ComparisonOptions.All | ComparisonOptions.AllowEqualsOverride;

            var diff = provider.ComputeDiff<ComplexDataSpec>(left, right, options,
                x => x.DateCreatedUtc,
                x => x.DateModifiedUtc,
                x => x.ChangesetItemHash,
                x => x.Id,
                x => x.Rules.Select(y => y.Id),
                x => x.DayParts.Select(y => y.BuySpecificationDayPartId),
                x => x.DayParts.Select(y => y.Budgets.Select(z => z.Id)),
                x => x.DayParts.Select(y => y.Rules.Select(z => z.Id)),
                // TODO: temp leave these ignores till Name and Code are removed from DspDayPart api model
                x => x.DayParts.Select(y => y.Name),
                x => x.DayParts.Select(y => y.Code));

            Assert.AreEqual(2, diff.Count);
        }

        [Test]
        public void ShouldDetect_ComplexJson_IncludeList()
        {
            var provider = new DiffProvider();

            var left = JsonConvert.DeserializeObject<ComplexDataSpec>(File.ReadAllText(GetFullPath(@"Data\ComplexJson3.json")));
            var right = JsonConvert.DeserializeObject<ComplexDataSpec>(File.ReadAllText(GetFullPath(@"Data\ComplexJson4.json")));

            var options = ComparisonOptions.All | ComparisonOptions.IncludeList | ComparisonOptions.AllowEqualsOverride | ComparisonOptions.AllowCollectionsToBeOutOfOrder;

            var diff = provider.ComputeDiff<ComplexDataSpec>(left, right, options,               
                // only diff the following properties (inclusion)
                x => x.PortfolioId,
                x => x.Name,
                x => x.Code,
                x => x.AdvertiserOrganizationId,
                x => x.BrandOrganizationId,
                x => x.MarketTypeId,
                x => x.MarketsIds,
                x => x.ExcludedProgramIds,
                x => x.ExcludedProgramCategoryIds,
                x => x.ExcludedStationOrganizationIds,
                x => x.PrimaryDemographicId,
                x => x.IsSent,
                x => x.ChangesetItemHash,
                x => x.ParentBuySpecificationId,
                x => x.IsClone,
                x => x.IsOutOfDate,
                x => x.TotalBrandBudget,
                x => x.TotalGRP,
                x => x.Budgets,
                x => x.Rules,
                x => x.AirtimeSeparationId,
                x => x.MaxSharePerStation,
                x => x.MinSharePerStation,
                x => x.MaxPerWeek
            );

            Assert.AreEqual(0, diff.Count);

            // perform the same comparison without AllowCollectionsToBeOutOfOrder
            options = ComparisonOptions.All | ComparisonOptions.IncludeList | ComparisonOptions.AllowEqualsOverride;

            diff = provider.ComputeDiff<ComplexDataSpec>(left, right, options,
                // only diff the following properties (inclusion)
                x => x.PortfolioId,
                x => x.Name,
                x => x.Code,
                x => x.AdvertiserOrganizationId,
                x => x.BrandOrganizationId,
                x => x.MarketTypeId,
                x => x.MarketsIds,
                x => x.ExcludedProgramIds,
                x => x.ExcludedProgramCategoryIds,
                x => x.ExcludedStationOrganizationIds,
                x => x.PrimaryDemographicId,
                x => x.IsSent,
                x => x.ChangesetItemHash,
                x => x.ParentBuySpecificationId,
                x => x.IsClone,
                x => x.IsOutOfDate,
                x => x.TotalBrandBudget,
                x => x.TotalGRP,
                x => x.Budgets,
                x => x.Rules,
                x => x.AirtimeSeparationId,
                x => x.MaxSharePerStation,
                x => x.MinSharePerStation,
                x => x.MaxPerWeek
            );

            Assert.AreEqual(0, diff.Count);
        }

        private string GetFullPath(string filename)
        {
            var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), filename);
            return path;
        }
    }
}

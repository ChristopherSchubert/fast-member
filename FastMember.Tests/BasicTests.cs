﻿using System;
using NUnit.Framework;
using System.Data;
using FastMember;

namespace FastMemberTests
{
    [TestFixture]
    public class BasicTests
    {
        [Test]
        public void BasicReadTest_PropsOnClass()
        {
            var now = DateTime.Now;

            var obj = new PropsOnClass() { A = 123, B = "abc", C = now, D = null };

            var access = TypeAccessor.Create(typeof(PropsOnClass));

            Assert.AreEqual(123, access[obj, "A"]);
            Assert.AreEqual("abc", access[obj, "B"]);
            Assert.AreEqual(now, access[obj, "C"]);
            Assert.AreEqual(null, access[obj, "D"]);
        }

        [Test]
        public void BasicWriteTest_PropsOnClass()
        {
            var now = DateTime.Now;

            var obj = new PropsOnClass();

            var access = TypeAccessor.Create(typeof(PropsOnClass));

            access[obj, "A"] = 123;
            access[obj, "B"] = "abc";
            access[obj, "C"] = now;
            access[obj, "D"] = null;

            Assert.AreEqual(123, obj.A);
            Assert.AreEqual("abc", obj.B);
            Assert.AreEqual(now, obj.C);
            Assert.AreEqual(null, obj.D);
        }

        [Test]
        public void Getmembers()
        {
            var access = TypeAccessor.Create(typeof(PropsOnClass));
            Assert.IsTrue(access.GetMembersSupported);
            var members = access.GetMembers();
            Assert.AreEqual(4, members.Count);
            Assert.AreEqual("A", members[0].Name);
            Assert.AreEqual("B", members[1].Name);
            Assert.AreEqual("C", members[2].Name);
            Assert.AreEqual("D", members[3].Name);
        }

        [Test]
        public void BasicReadTest_PropsOnClass_ViaWrapper()
        {
            var now = DateTime.Now;

            var obj = new PropsOnClass() { A = 123, B = "abc", C = now, D = null };

            var wrapper = ObjectAccessor.Create(obj);

            Assert.AreEqual(123, wrapper["A"]);
            Assert.AreEqual("abc", wrapper["B"]);
            Assert.AreEqual(now, wrapper["C"]);
            Assert.AreEqual(null, wrapper["D"]);
        }

        [Test]
        public void BasicWriteTest_PropsOnClass_ViaWrapper()
        {
            var now = DateTime.Now;

            var obj = new PropsOnClass();

            var wrapper = ObjectAccessor.Create(obj);

            wrapper["A"] = 123;
            wrapper["B"] = "abc";
            wrapper["C"] = now;
            wrapper["D"] = null;

            Assert.AreEqual(123, obj.A);
            Assert.AreEqual("abc", obj.B);
            Assert.AreEqual(now, obj.C);
            Assert.AreEqual(null, obj.D);
        }

        [Test]
        public void BasicReadTest_FieldsOnClass()
        {
            var now = DateTime.Now;

            var obj = new FieldsOnClass() { A = 123, B = "abc", C = now, D = null };

            var access = TypeAccessor.Create(typeof(FieldsOnClass));

            Assert.AreEqual(123, access[obj, "A"]);
            Assert.AreEqual("abc", access[obj, "B"]);
            Assert.AreEqual(now, access[obj, "C"]);
            Assert.AreEqual(null, access[obj, "D"]);
        }

        [Test]
        public void BasicWriteTest_FieldsOnClass()
        {
            var now = DateTime.Now;

            var obj = new FieldsOnClass();

            var access = TypeAccessor.Create(typeof(FieldsOnClass));

            access[obj, "A"] = 123;
            access[obj, "B"] = "abc";
            access[obj, "C"] = now;
            access[obj, "D"] = null;

            Assert.AreEqual(123, obj.A);
            Assert.AreEqual("abc", obj.B);
            Assert.AreEqual(now, obj.C);
            Assert.AreEqual(null, obj.D);
        }

        [Test]
        public void BasicReadTest_PropsOnStruct()
        {
            var now = DateTime.Now;

            var obj = new PropsOnStruct() { A = 123, B = "abc", C = now, D = null };

            var access = TypeAccessor.Create(typeof(PropsOnStruct));

            Assert.AreEqual(123, access[obj, "A"]);
            Assert.AreEqual("abc", access[obj, "B"]);
            Assert.AreEqual(now, access[obj, "C"]);
            Assert.AreEqual(null, access[obj, "D"]);
        }

        [Test]
        public void BasicWriteTest_PropsOnStruct()
        {
            var now = DateTime.Now;

            object obj = new PropsOnStruct { A = 1 };

            var access = TypeAccessor.Create(typeof(PropsOnStruct));

            access[obj, "A"] = 123;
            
            Assert.AreEqual(123, ((PropsOnStruct)obj).A);
        }

        [Test]
        public void BasicReadTest_FieldsOnStruct()
        {
            var now = DateTime.Now;

            var obj = new FieldsOnStruct() { A = 123, B = "abc", C = now, D = null };

            var access = TypeAccessor.Create(typeof(FieldsOnStruct));

            Assert.AreEqual(123, access[obj, "A"]);
            Assert.AreEqual("abc", access[obj, "B"]);
            Assert.AreEqual(now, access[obj, "C"]);
            Assert.AreEqual(null, access[obj, "D"]);
        }

        [Test]
        public void BasicWriteTest_FieldsOnStruct()
        {
            var now = DateTime.Now;

            object obj = new FieldsOnStruct();
            
            var access = TypeAccessor.Create(typeof(FieldsOnStruct));

            access[obj, "A"] = 123;
            Assert.AreEqual(123, ((FieldsOnStruct)obj).A);
        }

        [Test]
        public void WriteInvalidMember()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var access = TypeAccessor.Create(typeof(PropsOnClass));
                var obj = new PropsOnClass();
                access[obj, "doesnotexist"] = "abc";
            });
        }

        [Test]
        public void ReadInvalidMember()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var access = TypeAccessor.Create(typeof(PropsOnClass));
                var obj = new PropsOnClass();
                object value = access[obj, "doesnotexist"];
            });
        }

        [Test]
        public void GetSameAccessor()
        {
            var x = TypeAccessor.Create(typeof(PropsOnClass));
            var y = TypeAccessor.Create(typeof(PropsOnClass));
            Assert.AreSame(x, y);
        }

        public class PropsOnClass
        {
            public int A { get; set; }
            public string B { get; set; }
            public DateTime? C { get; set; }
            public decimal? D { get; set; }
        }
        public class FieldsOnClass
        {
            public int A;
            public string B;
            public DateTime? C;
            public decimal? D;
        }
        public struct PropsOnStruct
        {
            public int A { get; set; }
            public string B { get; set; }
            public DateTime? C { get; set; }
            public decimal? D { get; set; }
        }
        public struct FieldsOnStruct
        {
            public int A;
            public string B;
            public DateTime? C;
            public decimal? D;
        }


        public class HasDefaultCtor { }
        public class HasNoDefaultCtor { public HasNoDefaultCtor(string s) { } }
        public abstract class IsAbstract { }   

        [Test]
        public void TestCtor()
        {
            var accessor = TypeAccessor.Create(typeof(HasNoDefaultCtor));
            Assert.IsFalse(accessor.CreateNewSupported);

            accessor = TypeAccessor.Create(typeof(IsAbstract));
            Assert.IsFalse(accessor.CreateNewSupported);

            Assert.AreNotEqual("DynamicAccessor", accessor.GetType().Name);
            Assert.AreNotEqual("DelegateAccessor", accessor.GetType().Name);

            accessor = TypeAccessor.Create(typeof (HasDefaultCtor));
            Assert.IsTrue(accessor.CreateNewSupported);
            object obj = accessor.CreateNew();
            Assert.IsInstanceOf(typeof(HasDefaultCtor), obj);
        }

        public class HasGetterNoSetter
        {
            public int Foo { get { return 5; } }
        }
        [Test]
        public void TestHasGetterNoSetter()
        {
            var obj = new HasGetterNoSetter();
            var acc = TypeAccessor.Create(typeof (HasGetterNoSetter));
            Assert.AreEqual(5, acc[obj, "Foo"]);
        }
        public class HasGetterPrivateSetter
        {
            public int Foo { get; private set; }
            public HasGetterPrivateSetter(int value) { Foo = value; }
        }
        [Test]
        public void TestHasGetterPrivateSetter()
        {
            var obj = new HasGetterPrivateSetter(5);
            var acc = TypeAccessor.Create(typeof(HasGetterPrivateSetter));
            Assert.AreEqual(5, acc[obj, "Foo"]);
        }

        public class MixedAccess
        {
            public MixedAccess()
            {
                Foo = Bar = 2;
            }
            public int Foo { get; private set; }
            public int Bar { private get; set; }
        }

        [Test]
        public void TestMixedAccess()
        {
            TypeAccessor acc0 = TypeAccessor.Create(typeof(MixedAccess)),
                         acc1 = TypeAccessor.Create(typeof(MixedAccess), false),
                         acc2 = TypeAccessor.Create(typeof(MixedAccess), true);

            Assert.AreSame(acc0, acc1);
            Assert.AreNotSame(acc0, acc2);

            var obj = new MixedAccess();
            Assert.AreEqual(2, acc0[obj, "Foo"]);
            Assert.AreEqual(2, acc2[obj, "Foo"]);
            Assert.AreEqual(2, acc2[obj, "Bar"]);

            acc0[obj, "Bar"] = 3;
            Assert.AreEqual(3, acc2[obj, "Bar"]);
            acc2[obj, "Bar"] = 4;
            Assert.AreEqual(4, acc2[obj, "Bar"]);
            acc2[obj, "Foo"] = 5;
            Assert.AreEqual(5, acc0[obj, "Foo"]);

            try
            {
                int i = (int)acc0[obj, "Bar"];
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException)
            { } // fine
            try
            {
                acc0[obj, "Foo"] = 6;
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException)
            { } // fine
        }

        public class ObjectReaderType {
            public int A {get;set;}
            public string B {get;set;}
            public byte C {get;set;}
            public int? D { get; set; }
        }

    }
}

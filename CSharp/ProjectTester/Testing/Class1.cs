﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTester;
using NUnit.Framework;
using NUnit.Framework.Legacy;
namespace Testing
{
    [TestFixture]
    public class Class1
    {
        //arrange
        Program p = new Program();

        [Test]
        //1. test case
        public void Test_Login()
        {
            //Act
            string s1 = p.Login("Banurekha", "aaa");
            string s2 = p.Login("", "");
            string s3 = p.Login("Admin", "Admin@123");

            //Assert
            ClassicAssert.AreEqual("UserId or Password cannot be empty", s2);
            ClassicAssert.AreEqual("Incorrect User Id or Password", s1);
            ClassicAssert.AreEqual("Welcome Admin", s3);
        }

        [Test]
        public void CheckEmployeeDetails()
        {
            List<Employee> employeelist = p.EmpData();
            foreach (var x in employeelist)
            {
                ClassicAssert.IsNotNull(x.Id);
                ClassicAssert.IsNotNull(x.Name);
            }
        }

        [Test]
        [TestCase(15, 35, 50)]
        [TestCase(10, 45, 55)]
        [TestCase(20, 50, 70)]

        public void TestAdd2Nos_withTestCases(int n1, int n2, int expected)
        {
            int result = p.Add2Nos(n1, n2);
            ClassicAssert.AreEqual(expected, result);
        }
    }
}
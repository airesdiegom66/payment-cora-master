using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payment.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.Tests.ValueObjects
{
    //Red: fazer falhar, Green: fazer passar, Refactor: refatorar o código
    [TestClass]
    public class DocumentTests
    {
        [TestMethod]
        public void Should_Return_Error_When_Cpf_Is_Invalid()
        {
            var document = new Document("012345");
            Assert.IsTrue(document.Invalid);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("52893246079")]
        [DataRow("99039991006")]
        [DataRow("62122413093")]
        [DataRow("857.021.640-83")]
        [DataRow("337.522.020-09")]
        public void Should_Return_Success_When_Cpf_Is_Valid(string cpf)
        {
            var document = new Document(cpf);
            Assert.IsTrue(document.Valid);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using RallyDakar.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace RallyDakar.Dominio.Testes.Equipes
{
    [TestClass]
    public class EquipeValidacaoOkTeste
    {
        Equipe equipe1;

        [TestMethod]
        public void EquipeValidadoCorretamente()
        {
            equipe1 = new Equipe()
            {
                CodigoIdentificador = "KTM",
                Nome = "EquipeTest"
            };

            Assert.IsTrue(equipe1.Validado());
        }
    }
}

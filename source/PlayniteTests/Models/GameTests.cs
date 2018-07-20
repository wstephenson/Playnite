﻿using NUnit.Framework;
using Playnite;
using Playnite.SDK.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayniteTests.Models
{
    [TestFixture]
    public class GameTests
    {
        [Test]
        public void ResolveVariablesTest()
        {
            var dir = @"c:\test\test2\";
            var game = new Game()
            {
                Name = "test game",
                InstallDirectory = dir,
                IsoPath = Path.Combine(dir, "test.iso")
            };

            Assert.AreEqual(string.Empty, game.ExpandVariables(string.Empty));
            Assert.AreEqual("teststring", game.ExpandVariables("teststring"));
            Assert.AreEqual(dir + "teststring", game.ExpandVariables("{InstallDir}teststring"));
            Assert.AreEqual(game.InstallDirectory, game.ExpandVariables("{InstallDir}"));
            Assert.AreEqual(game.IsoPath, game.ExpandVariables("{ImagePath}"));
            Assert.AreEqual("test", game.ExpandVariables("{ImageNameNoExt}"));
            Assert.AreEqual("test.iso", game.ExpandVariables("{ImageName}"));
            Assert.AreEqual(Paths.ProgramPath, game.ExpandVariables("{PlayniteDir}"));
            Assert.AreEqual("test game", game.ExpandVariables("{Name}"));
            Assert.AreEqual("test2", game.ExpandVariables("{InstallDirName}"));
        }

        [Test]
        public void ResolveVariablesEmptyTest()
        {
            // Should not throw
            var game = new Game();
            game.ExpandVariables(string.Empty);
            game.ExpandVariables(null);
        }
    }
}

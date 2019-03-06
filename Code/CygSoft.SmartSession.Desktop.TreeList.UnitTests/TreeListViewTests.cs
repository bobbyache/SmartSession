﻿using CygSoft.SmartSession.Desktop.TreeList.Tree;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Desktop.TreeList.UnitTests
{
    [TestFixture]
    public class TreeListViewTests
    {
        [Test]
        [Apartment(ApartmentState.STA)]
        public void Test()
        {
            var treeListView = new TreeListView();
            var animals = AnimalTree.GetTree(treeListView);

            var earthworm =
            animals
                .Children[0/*Invertebrates*/]
                    .Children[1/*Without Legs*/]
                        .Children[1/*Worm-like*/]
                            .Children[0/*Earthworm*/];

            treeListView.SelectedItem = earthworm;

            Assert.AreEqual(treeListView.SelectedItem, earthworm);
        }

        private class TestTreeListView : TreeListView
        {
            
        }
    }
}
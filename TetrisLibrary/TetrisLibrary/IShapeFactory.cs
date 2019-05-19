using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisLibrary
{
    /// Represents the factory used to create a new Shape.
    public interface IShapeFactory
    {
        /// Randomly creates a new Shape.
        void DeployNewShape();
    }
}
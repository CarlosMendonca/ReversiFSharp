namespace Reversi.Tests

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open Reversi

[<TestClass>]
type BoardTests () =

    [<TestMethod>]
    member this.CanInitializeBoard () =
        let board = new Board()

        Assert.AreEqual(board.squares.[3,3], BoardSquare.Played(Piece.Black))
        Assert.AreEqual(board.squares.[4,4], BoardSquare.Played(Piece.Black))
        Assert.AreEqual(board.squares.[3,4], BoardSquare.Played(Piece.White))
        Assert.AreEqual(board.squares.[4,3], BoardSquare.Played(Piece.White))

        // Make sure default size is 8x8 because reasons
        Assert.AreEqual(
            (board.squares |> Array2D.length1, board.squares |> Array2D.length2),
            (8, 8))
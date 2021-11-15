namespace Reversi.Tests

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open Reversi

[<TestClass>]
type BoardTests () =

    [<TestMethod>]
    member this.CanInitializeBoard () =
        let boardSize = 8
        let board = new Board(boardSize)

        Assert.AreEqual(board.squares.[3,3], BoardSquare.Played(Piece.Black))
        Assert.AreEqual(board.squares.[4,4], BoardSquare.Played(Piece.Black))
        Assert.AreEqual(board.squares.[3,4], BoardSquare.Played(Piece.White))
        Assert.AreEqual(board.squares.[4,3], BoardSquare.Played(Piece.White))

        // Make sure default size is 8x8 because reasons
        Assert.AreEqual(
            (board.squares |> Array2D.length1, board.squares |> Array2D.length2),
            (boardSize, boardSize))

    [<TestMethod>]
    member this.CapturePiecesCorrectly () =
        let board = new Board(8)
        let coords = [|(3,2); (3,3)|]

        board.SetSquares(coords, Piece.White)

        Assert.AreEqual(board.squares.[3,2], BoardSquare.Played(Piece.White))
        Assert.AreEqual(board.squares.[3,3], BoardSquare.Played(Piece.White))

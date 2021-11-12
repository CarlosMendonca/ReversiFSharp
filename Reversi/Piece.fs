namespace Reversi

    type Piece = Black | White with
        member this.Stringify =
            match this with
            | Black -> "B"
            | White -> "W"

    type BoardSquare = Played of Piece | Unplayed | OutOfBounds with
        member this.Stringify =
            match this with
            | Played piece -> piece.Stringify
            | Unplayed    -> " "
            | OutOfBounds -> "?"

    type CoordinatedBoardSquare =
        { Coord  : int * int
          Square : BoardSquare }
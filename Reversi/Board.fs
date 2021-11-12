namespace Reversi

type Board =
    val internal squares : BoardSquare[,]

    new(board_size : int) = {
        squares = Board.SquareFactory(board_size)
    }

    member this.GetCoordSquareAt(coord : int * int) =
        this.GetCordSquareTowards(coord, (0,0), 0)

    member this.GetCordSquareTowards((coordX,coordY) : int * int, (vectorX, vectorY) : int * int, hops : int) = 
        let targetCoord = (vectorX * hops + coordX, vectorY * hops + coordY)
        let (boundaryCoordX, boundaryCoordY) = ((this.squares |> Array2D.length1) - 1, (this.squares |> Array2D.length2) - 1)

        match targetCoord with
        | (x, y) when x < 0 || y < 0 || x > boundaryCoordX || y > boundaryCoordY ->
            { Coord = targetCoord; Square = BoardSquare.OutOfBounds }
        | _ ->
            { CoordinatedBoardSquare.Coord = targetCoord; Square = this.squares |> Array2D.get <|| targetCoord }
    
    member this.SetSquares(coords : (int * int)[], player : Piece) =
        coords |> Array.iter (fun (coordX, coordY) -> this.squares.[coordX,coordY] <- BoardSquare.Played(player))

    member this.Stringify =
        let header      = "   | A | B | C | D | E | F | G | H |"
        let row_divider = "---+---+---+---+---+---+---+---+---+"

        let theString = new System.Text.StringBuilder()
        theString
            .AppendLine(header)
            .AppendLine(row_divider) |> ignore

        for i in [0 .. (this.squares |> Array2D.length1) - 1] do
            theString.Append($" {i+1} |") |> ignore
            this.squares[i, *] |> Array.iter (fun v -> theString.Append($" {v.Stringify} |") |> ignore)
            theString
                .AppendLine()
                .AppendLine(row_divider) |> ignore

        theString.ToString()

    static member private SquareFactory(board_size : int) =
        let squares = Array2D.create board_size board_size BoardSquare.Unplayed
        squares.[3,3] <- BoardSquare.Played(Piece.Black)
        squares.[4,4] <- BoardSquare.Played(Piece.Black)
        squares.[3,4] <- BoardSquare.Played(Piece.White)
        squares.[4,3] <- BoardSquare.Played(Piece.White)

        squares
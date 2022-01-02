namespace Reversi
    
    type Game =
        val internal board : Board
        val internal availablePositions : ValidPlay[]
        val mutable internal currentPlayer : Piece

        new() = {
            board = new Board(8)
            currentPlayer = Piece.White
            availablePositions = Array.empty

            // TO-DO: this.CheckAvailablePositions()
        }

        static member internal DirectionVectors = [|
            ( 0, 1);
            ( 1, 0);
            ( 1, 1);
            ( 0,-1);
            (-1, 0);
            (-1,-1);
            ( 1,-1);
            (-1, 1); |]

        member this.IsGameOver =
            this.availablePositions.Length = 0 // if no positions are available to play, then game is over and currentPlayer wins

        member internal this.CurrentOpponent =
            match this.currentPlayer with
            | Piece.Black -> Piece.White
            | Piece.White -> Piece.Black

        member this.TryPlay(coord : int*int) =
            // Iterate over the the array of available positions and if this coordinate is there, have the current player play it
            if (this.availablePositions |> Array.exists (fun position -> position.Coord = coord)) then
                let positionToPlay = this.availablePositions |> Array.find (fun position -> position.Coord = coord)
                
                // Flip all changed coordinates, including the coordinate played
                let coordsToFlip = [|[|coord|]; positionToPlay.ChangedCoords|] |> Array.concat

                this.board.SetSquares(coordsToFlip, this.currentPlayer)
                this.currentPlayer <- this.CurrentOpponent
                this.CheckAvailablePositions

                Ok $"{this.currentPlayer} played {coord}"
            else
                Error "Invalid play"

        member internal this.CheckAvailablePositions =
            this.board
            failwith "NOT YET IMPLEMENTED!"

        member internal this.CheckPlay =
            PlayResult.Invalid

        //member this.GetCapturedCoords(coord : int * int, vector : int * int) =
        //    let hops = 1
        //    let switchableCoords : (int * int)[] = Array.empty
        //    ()

        //member this.IncrementHop(coord : int * int, vector : int * int, hops : int, switchableCoords : (int * int)[]) = 
        //    let currentCoordSquare = this.board.GetCordSquareTowards(coord, vector, hops)

        //    match currentCoordSquare.Square with
        //    | BoardSquare.Played(piece) ->
        //        this.IncrementHop(coord, vector, hops+1, (switchableCoords |> Array.append currentCoordSquare.Coord))



        //        //match currentCoordSquare.Square with
        //        //| BoardSquare.Played(piece) ->
        //        //    match piece with
        //        //    | this.CurrentOpponent ->
        //        //        hops++
        //        //        switchableCoords |> Array.append currentCoordSquare.Coord
        //        //    | _ -> switchableCoords
        //        //| _ -> Array.empty

        //member this.CheckPlay(coord : int * int) = 
        //    match this.board.GetCoordSquareAt(coord).Square with
        //    | Reversi.BoardSquare.Played _ -> PlayResult.Invalid
        //    | Reversi.BoardSquare.OutOfBounds -> PlayResult.Invalid
        //    | Reversi.BoardSquare.Unplayed -> 
        //        let capturedCoords : (int * int)[] = Array.empty
        //        let vectors = Game.DirectionVectors
        //        vectors |> Array.iter (fun vector -> capturedCoords |> Array.append this.GetCapturedCoords(coord, vector))

        //        PlayResult.Invalid


        //member this.CheckAvailablePositions =
        //    ()
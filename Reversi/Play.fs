namespace Reversi
    
    type ValidPlay = {
        Score : int
        Coord : int * int
        ChangedCoords : (int * int)[]
        Player : Piece }

    type PlayResult = ValidWithScore of ValidPlay | Invalid | Undefined
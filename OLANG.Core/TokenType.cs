namespace OLANG.Core;

public enum TokenType
{
    Plus, Minus, Star, Slash, 
    OpenParen, CloseParen,
    Equals,

    Identifier, 
    Number,    
    String,    

   
    Var, If, Else, True, False, Print,

    
    EOF,        
    BadToken    
}
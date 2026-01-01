namespace OLANG.Core;

public enum TokenType : byte
{
    Plus, Minus, Star, Slash, 
    OpenParen, CloseParen,
    Equals,

    Identifier, 
    Number,    
    String,    
    Bang,
    BangEquals,
    EqualsEquals,
    Less,
    LessOrEquals,
    Greater,
    GreaterOrEquals,

    Var, If, Else, True, False, Print,

    
    EOF,        
    BadToken,
    Comma
}
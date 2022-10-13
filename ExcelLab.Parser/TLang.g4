grammar TLang;


content: expression EOF;

expression : (op = (PLUSPLUS | MINUSMINUS)) expression #decinc
| expression '!' #factorial
| expression '^' expression #exponent
| expression (op = (ASTERISK | SLASH)) expression #muldiv
| expression (op = (PLUS | MINUS)) expression #addsub
| '('expression')' #parentheses
| (PLUS | MINUS)? FLOAT #number;

FLOAT : DIGITS+('.'DIGITS+)? ;
PLUSPLUS: PLUS PLUS;
MINUSMINUS: MINUS MINUS;
PLUS : '+' ;
MINUS : '-' ;
ASTERISK : '*';
SLASH : '/';
fragment DIGITS : [0-9] ;

WS : [ \r\n\t] + -> skip ;
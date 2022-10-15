grammar TLang;


content: expression EOF;

expression : (op = (MAX | MIN)) '(' exps+=expression (',' exps+= expression)*')' #minmax
| AVG '(' exps+=expression (',' exps+= expression)*')' #avg
| SUM '(' exps+=expression (',' exps+= expression)*')' #sum
| IF '(' condition=expression ',' ifbranch=expression ',' elsebranch=expression ')' #if
| CMP '(' expression ',' expression ')' #cmp
| (op = (PLUSPLUS | MINUSMINUS)) expression #decinc
| expression '!' #factorial
| expression '^' expression #exponent
| expression (op = (ASTERISK | SLASH)) expression #muldiv
| expression (op = (PLUS | MINUS)) expression #addsub
| '('expression')' #parentheses
| (PLUS | MINUS)? FLOAT #number;

SUM : 'SUM';
AVG : 'AVG';
MAX : 'MAX';
MIN : 'MIN';
IF : 'IF';
CMP : 'CMP';
FLOAT : DIGITS+('.'DIGITS+)? ;
PLUSPLUS: PLUS PLUS;
MINUSMINUS: MINUS MINUS;
PLUS : '+' ;
MINUS : '-' ;
ASTERISK : '*';
SLASH : '/';
fragment DIGITS : [0-9] ;

WS : [ \r\n\t] + -> skip ;
UKNOWNCHAR : . ;
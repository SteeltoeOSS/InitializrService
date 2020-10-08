# Grammars

## Expression

```
<expression>        := <operand> | <operand> <operator> <expression>
<operand>           := <parameter> | <function> | <integer>
<parameter          := <name>
<parameter-list>    := <parameter> | <parameter> "," <parameter-list>
<function>          := <name> "(" ")" | <name> "(" <parameter-list> ")"
<name>              := <letter> | <letter> <char-list>
<char-list>         := <char> | <char> <char-list>
<char>              := <letter> | <digit> | <special>
<letter>            :=  <upper> | <lower>
<upper>             := "A" | "B" | ... | "Z"
<lower>             := "a" | "b" | ... | "z"
<integer>           := <digit> | <digit> <integer>
<digit>             := "0" | "1" | ... | "9"
<special>           := "-"
<operator>          := "||"
```

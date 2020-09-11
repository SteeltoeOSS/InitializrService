# Grammars

## Expression

```
<expression>        := <operand> | <operand> <operator> <operand>
<operator>          := "||"
<operand>           := <parameter> | <function>
<parameter          := <upper> | <upper> <letters-or-digits>
<function>          := <fname> "(" <parameter-list> ")"
<fname>             := <lower> | <lower> <letters-or-digits>
<parameter-list>    := <parameter> | <parameter> "," <parameter-list>
<letters-or-digits> := <letter-or-digit> | <letter-or-digit> <letters-or-digits>
<letter-or-digit>   := <letter> | <digit>
<letter>            :=  <upper> | <lower>
<upper>             := "A" | "B" | ... | "Z"
<lower>             := "a" | "b" | ... | "z"
<digit>             := "0" | "1" | ... | "9"
```

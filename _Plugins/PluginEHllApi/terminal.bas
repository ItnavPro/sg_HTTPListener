Print("terminal.bas start")
Print("p1=" + p1)
Print("p2=" + p2)

let x1=connect(p1)

let x5=readscreen(13,9)
Print("x5=" + x5)
If x5 = "שלום מאיה" Then
	Print("YES")
	let y1=165
	let x2=setcursorpos(y1)
	let x3=getcursorpos()
	Print("x3=" + x3)
	let x4=sendstr(p1)	
	For i = 1 To 4
		let y1=165 + 80 * i
		let x2=setcursorpos(y1)
		let x4=sendstr(p1 + str(i))
	Next i	
Else
	Print("NO")
EndIf

Print("terminal.bas stop")
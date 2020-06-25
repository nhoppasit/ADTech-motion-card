Imports ADTMotionControl
Public Class frmIOTest

#Region "Scan timer"

    Private Sub TimerScan_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerScan.Tick
        'CheckInput()
        'CheckOut()
    End Sub

    'Input detected' ADT-8960 card has 44 input points (0 ~ 43)
    'This procedure is a signal input to low
    'Private Sub CheckInput()
    '    Dim value As Short

    '    value = CtrlCard.adt8940a1_Read_Input(0) : chkXLMTP.Checked = (value = 0)
    '    value = CtrlCard.adt8940a1_Read_Input(1) : chkXLMTN.Checked = (value = 0)
    '    value = CtrlCard.adt8940a1_Read_Input(2) : chkXSTOP0.Checked = (value = 0)
    '    value = CtrlCard.adt8940a1_Read_Input(3) : chkXSTOP1.Checked = (value = 0)

    '    value = CtrlCard.adt8940a1_Read_Input(4) : chkYLMTP.Checked = (value = 0)
    '    value = CtrlCard.adt8940a1_Read_Input(5) : chkYLMTN.Checked = (value = 0)
    '    value = CtrlCard.adt8940a1_Read_Input(6) : chkYSTOP0.Checked = (value = 0)
    '    value = CtrlCard.adt8940a1_Read_Input(7) : chkYSTOP1.Checked = (value = 0)

    '    value = CtrlCard.adt8940a1_Read_Input(8) : chkZLMTP.Checked = (value = 0)
    '    value = CtrlCard.adt8940a1_Read_Input(9) : chkZLMTN.Checked = (value = 0)
    '    value = CtrlCard.adt8940a1_Read_Input(10) : chkZSTOP0.Checked = (value = 0)
    '    value = CtrlCard.adt8940a1_Read_Input(11) : chkZSTOP1.Checked = (value = 0)

    '    value = CtrlCard.adt8940a1_Read_Input(12) : chkALMTP.Checked = (value = 0)
    '    value = CtrlCard.adt8940a1_Read_Input(13) : chkALMTN.Checked = (value = 0)
    '    value = CtrlCard.adt8940a1_Read_Input(14) : chkASTOP0.Checked = (value = 0)
    '    value = CtrlCard.adt8940a1_Read_Input(15) : chkASTOP1.Checked = (value = 0)

    '    value = CtrlCard.adt8940a1_Read_Input(16) : chkBLMTP.Checked = (value = 0)
    '    value = CtrlCard.adt8940a1_Read_Input(17) : chkBLMTN.Checked = (value = 0)
    '    value = CtrlCard.adt8940a1_Read_Input(18) : chkBSTOP0.Checked = (value = 0)
    '    value = CtrlCard.adt8940a1_Read_Input(19) : chkBSTOP1.Checked = (value = 0)

    '    value = CtrlCard.adt8940a1_Read_Input(20) : chkCLMTP.Checked = (value = 0)
    '    value = CtrlCard.adt8940a1_Read_Input(21) : chkCLMTN.Checked = (value = 0)
    '    value = CtrlCard.adt8940a1_Read_Input(22) : chkCSTOP0.Checked = (value = 0)
    '    value = CtrlCard.adt8940a1_Read_Input(23) : chkCSTOP1.Checked = (value = 0)

    '    value = CtrlCard.adt8940a1_Read_Input(24) : chkIN24.Checked = (value = 0)
    '    value = CtrlCard.adt8940a1_Read_Input(25) : chkIN25.Checked = (value = 0)
    '    value = CtrlCard.adt8940a1_Read_Input(26) : chkIN26.Checked = (value = 0)
    '    value = CtrlCard.adt8940a1_Read_Input(27) : chkIN27.Checked = (value = 0)

    '    value = CtrlCard.adt8940a1_Read_Input(28) : chkIN28.Checked = (value = 0)
    '    value = CtrlCard.adt8940a1_Read_Input(29) : chkIN29.Checked = (value = 0)
    '    value = CtrlCard.adt8940a1_Read_Input(30) : chkIN30.Checked = (value = 0)
    '    value = CtrlCard.adt8940a1_Read_Input(31) : chkIN31.Checked = (value = 0)

    '    value = CtrlCard.adt8940a1_Read_Input(32) : chkXECA.Checked = (value = 0)
    '    value = CtrlCard.adt8940a1_Read_Input(33) : chkXECB.Checked = (value = 0)

    '    value = CtrlCard.adt8940a1_Read_Input(34) : chkYECA.Checked = (value = 0)
    '    value = CtrlCard.adt8940a1_Read_Input(35) : chkYECB.Checked = (value = 0)

    '    value = CtrlCard.adt8940a1_Read_Input(36) : chkZECA.Checked = (value = 0)
    '    value = CtrlCard.adt8940a1_Read_Input(37) : chkZECB.Checked = (value = 0)

    '    value = CtrlCard.adt8940a1_Read_Input(38) : chkAECA.Checked = (value = 0)
    '    value = CtrlCard.adt8940a1_Read_Input(39) : chkAECB.Checked = (value = 0)

    '    value = CtrlCard.adt8940a1_Read_Input(40) : chkBECA.Checked = (value = 0)
    '    value = CtrlCard.adt8940a1_Read_Input(41) : chkBECB.Checked = (value = 0)

    '    value = CtrlCard.adt8940a1_Read_Input(42) : chkCECA.Checked = (value = 0)
    '    value = CtrlCard.adt8940a1_Read_Input(43) : chkCECB.Checked = (value = 0)

    'End Sub

    'Output single point
    ' ADT-8960 card with 16 output points (0 ~ 15)
    'This procedure is high for the button is pressed, when low bounce
    'Private Sub CheckOut()

    '    CtrlCard.adt8940a1_Write_Output(0, IIf(chkOUT0.Checked, 1, 0))
    '    CtrlCard.adt8940a1_Write_Output(1, IIf(chkOUT1.Checked, 1, 0))
    '    CtrlCard.adt8940a1_Write_Output(2, IIf(chkOUT2.Checked, 1, 0))
    '    CtrlCard.adt8940a1_Write_Output(3, IIf(chkOUT3.Checked, 1, 0))

    '    CtrlCard.adt8940a1_Write_Output(4, IIf(chkOUT4.Checked, 1, 0))
    '    CtrlCard.adt8940a1_Write_Output(5, IIf(chkOUT5.Checked, 1, 0))
    '    CtrlCard.adt8940a1_Write_Output(6, IIf(chkOUT6.Checked, 1, 0))
    '    CtrlCard.adt8940a1_Write_Output(7, IIf(chkOUT7.Checked, 1, 0))

    '    CtrlCard.adt8940a1_Write_Output(8, IIf(chkOUT8.Checked, 1, 0))
    '    CtrlCard.adt8940a1_Write_Output(9, IIf(chkOUT9.Checked, 1, 0))
    '    CtrlCard.adt8940a1_Write_Output(10, IIf(chkOUT10.Checked, 1, 0))
    '    CtrlCard.adt8940a1_Write_Output(11, IIf(chkOUT11.Checked, 1, 0))

    '    CtrlCard.adt8940a1_Write_Output(12, IIf(chkOUT12.Checked, 1, 0))
    '    CtrlCard.adt8940a1_Write_Output(13, IIf(chkOUT13.Checked, 1, 0))
    '    'CtrlCard.adt8940a1_Write_Output(14, IIf(chkOUT14.Checked, 1, 0))
    '    CtrlCard.adt8940a1_Write_Output(15, IIf(chkOUT15.Checked, 1, 0))

    'End Sub

#End Region

#Region "Output"

    'Sub AllOutputOpen()

    '    chkOUT0.Checked = True : CtrlCard.adt8940a1_Write_Output(0, 1)
    '    chkOUT1.Checked = True : CtrlCard.adt8940a1_Write_Output(1, 1)
    '    chkOUT2.Checked = True : CtrlCard.adt8940a1_Write_Output(2, 1)
    '    chkOUT3.Checked = True : CtrlCard.adt8940a1_Write_Output(3, 1)

    '    chkOUT4.Checked = True : CtrlCard.adt8940a1_Write_Output(4, 1)
    '    chkOUT5.Checked = True : CtrlCard.adt8940a1_Write_Output(5, 1)
    '    chkOUT6.Checked = True : CtrlCard.adt8940a1_Write_Output(6, 1)
    '    chkOUT7.Checked = True : CtrlCard.adt8940a1_Write_Output(7, 1)

    '    chkOUT8.Checked = True : CtrlCard.adt8940a1_Write_Output(8, 1)
    '    chkOUT9.Checked = True : CtrlCard.adt8940a1_Write_Output(9, 1)
    '    chkOUT10.Checked = True : CtrlCard.adt8940a1_Write_Output(10, 1)
    '    chkOUT11.Checked = True : CtrlCard.adt8940a1_Write_Output(11, 1)

    '    chkOUT12.Checked = True : CtrlCard.adt8940a1_Write_Output(12, 1)
    '    chkOUT13.Checked = True : CtrlCard.adt8940a1_Write_Output(13, 1)
    '    chkOUT14.Checked = True : CtrlCard.adt8940a1_Write_Output(14, 1)
    '    chkOUT15.Checked = True : CtrlCard.adt8940a1_Write_Output(15, 1)

    'End Sub

    'Sub AllOutputClose()

    '    chkOUT0.Checked = False : CtrlCard.adt8940a1_Write_Output(0, 0)
    '    chkOUT1.Checked = False : CtrlCard.adt8940a1_Write_Output(1, 0)
    '    chkOUT2.Checked = False : CtrlCard.adt8940a1_Write_Output(2, 0)
    '    chkOUT3.Checked = False : CtrlCard.adt8940a1_Write_Output(3, 0)

    '    chkOUT4.Checked = False : CtrlCard.adt8940a1_Write_Output(4, 0)
    '    chkOUT5.Checked = False : CtrlCard.adt8940a1_Write_Output(5, 0)
    '    chkOUT6.Checked = False : CtrlCard.adt8940a1_Write_Output(6, 0)
    '    chkOUT7.Checked = False : CtrlCard.adt8940a1_Write_Output(7, 0)

    '    chkOUT8.Checked = False : CtrlCard.adt8940a1_Write_Output(8, 0)
    '    chkOUT9.Checked = False : CtrlCard.adt8940a1_Write_Output(9, 0)
    '    chkOUT10.Checked = False : CtrlCard.adt8940a1_Write_Output(10, 0)
    '    chkOUT11.Checked = False : CtrlCard.adt8940a1_Write_Output(11, 0)

    '    chkOUT12.Checked = False : CtrlCard.adt8940a1_Write_Output(12, 0)
    '    chkOUT13.Checked = False : CtrlCard.adt8940a1_Write_Output(13, 0)
    '    chkOUT14.Checked = False : CtrlCard.adt8940a1_Write_Output(14, 0)
    '    chkOUT15.Checked = False : CtrlCard.adt8940a1_Write_Output(15, 0)

    'End Sub

    Private Sub btnAllOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAllOpen.Click
        'AllOutputOpen()
    End Sub

    Private Sub btnAllClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAllClose.Click
        'AllOutputClose()
    End Sub

#End Region

End Class
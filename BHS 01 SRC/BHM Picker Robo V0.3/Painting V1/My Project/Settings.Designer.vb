﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.1
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Namespace My
    
    <Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0"),  _
     Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
    Partial Friend NotInheritable Class MySettings
        Inherits Global.System.Configuration.ApplicationSettingsBase
        
        Private Shared defaultInstance As MySettings = CType(Global.System.Configuration.ApplicationSettingsBase.Synchronized(New MySettings()),MySettings)
        
#Region "My.Settings Auto-Save Functionality"
#If _MyType = "WindowsForms" Then
    Private Shared addedHandler As Boolean

    Private Shared addedHandlerLockObject As New Object

    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _
    Private Shared Sub AutoSaveSettings(ByVal sender As Global.System.Object, ByVal e As Global.System.EventArgs)
        If My.Application.SaveMySettingsOnExit Then
            My.Settings.Save()
        End If
    End Sub
#End If
#End Region
        
        Public Shared ReadOnly Property [Default]() As MySettings
            Get
                
#If _MyType = "WindowsForms" Then
               If Not addedHandler Then
                    SyncLock addedHandlerLockObject
                        If Not addedHandler Then
                            AddHandler My.Application.Shutdown, AddressOf AutoSaveSettings
                            addedHandler = True
                        End If
                    End SyncLock
                End If
#End If
                Return defaultInstance
            End Get
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property UseCard() As Boolean
            Get
                Return CType(Me("UseCard"),Boolean)
            End Get
            Set
                Me("UseCard") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("F:\ENERGETIC ENGINEERING\PR ROBOT\matlab_code")>  _
        Public Property MATLABDIR() As String
            Get
                Return CType(Me("MATLABDIR"),String)
            End Get
            Set
                Me("MATLABDIR") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property IsRepeatTestRun() As Boolean
            Get
                Return CType(Me("IsRepeatTestRun"),Boolean)
            End Get
            Set
                Me("IsRepeatTestRun") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("101")>  _
        Public Property LinearInterpIncrementStep() As Integer
            Get
                Return CType(Me("LinearInterpIncrementStep"),Integer)
            End Get
            Set
                Me("LinearInterpIncrementStep") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("12800")>  _
        Public Property TableR0PPR() As Long
            Get
                Return CType(Me("TableR0PPR"),Long)
            End Get
            Set
                Me("TableR0PPR") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("6400")>  _
        Public Property RotaryR1PPR() As String
            Get
                Return CType(Me("RotaryR1PPR"),String)
            End Get
            Set
                Me("RotaryR1PPR") = value
            End Set
        End Property
        
        <Global.System.Configuration.ApplicationScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.SpecialSettingAttribute(Global.System.Configuration.SpecialSetting.ConnectionString),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Data Source=C:\Database\dbPaintingV1.sdf")>  _
        Public ReadOnly Property dbPaintingV1ConnectionString() As String
            Get
                Return CType(Me("dbPaintingV1ConnectionString"),String)
            End Get
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("thunder")>  _
        Public Property DeleteCode() As String
            Get
                Return CType(Me("DeleteCode"),String)
            End Get
            Set
                Me("DeleteCode") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("thunder")>  _
        Public Property EditCode() As String
            Get
                Return CType(Me("EditCode"),String)
            End Get
            Set
                Me("EditCode") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("thunder")>  _
        Public Property AddCode() As String
            Get
                Return CType(Me("AddCode"),String)
            End Get
            Set
                Me("AddCode") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("thunder")>  _
        Public Property ValveEditCode() As String
            Get
                Return CType(Me("ValveEditCode"),String)
            End Get
            Set
                Me("ValveEditCode") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("thunder")>  _
        Public Property SpeedEditCode() As String
            Get
                Return CType(Me("SpeedEditCode"),String)
            End Get
            Set
                Me("SpeedEditCode") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("2")>  _
        Public Property TableHomeOffset() As Double
            Get
                Return CType(Me("TableHomeOffset"),Double)
            End Get
            Set
                Me("TableHomeOffset") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0")>  _
        Public Property R11HomeOffset() As Double
            Get
                Return CType(Me("R11HomeOffset"),Double)
            End Get
            Set
                Me("R11HomeOffset") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0")>  _
        Public Property R12HomeOffset() As Double
            Get
                Return CType(Me("R12HomeOffset"),Double)
            End Get
            Set
                Me("R12HomeOffset") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("#0.00")>  _
        Public Property FFormat() As String
            Get
                Return CType(Me("FFormat"),String)
            End Get
            Set
                Me("FFormat") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("6400")>  _
        Public Property RotaryR2PPR() As String
            Get
                Return CType(Me("RotaryR2PPR"),String)
            End Get
            Set
                Me("RotaryR2PPR") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("thunder")>  _
        Public Property PressureEditCode() As String
            Get
                Return CType(Me("PressureEditCode"),String)
            End Get
            Set
                Me("PressureEditCode") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0.9")>  _
        Public Property PresureMax() As Double
            Get
                Return CType(Me("PresureMax"),Double)
            End Get
            Set
                Me("PresureMax") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0.005")>  _
        Public Property PressureMin() As Double
            Get
                Return CType(Me("PressureMin"),Double)
            End Get
            Set
                Me("PressureMin") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("97")>  _
        Public Property TwistPlusLimit() As Long
            Get
                Return CType(Me("TwistPlusLimit"),Long)
            End Get
            Set
                Me("TwistPlusLimit") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("-25")>  _
        Public Property TwistMinusLimit() As Long
            Get
                Return CType(Me("TwistMinusLimit"),Long)
            End Get
            Set
                Me("TwistMinusLimit") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("76")>  _
        Public Property BendingPlusLimit() As Long
            Get
                Return CType(Me("BendingPlusLimit"),Long)
            End Get
            Set
                Me("BendingPlusLimit") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("-143")>  _
        Public Property BendingMinusLimit() As Long
            Get
                Return CType(Me("BendingMinusLimit"),Long)
            End Get
            Set
                Me("BendingMinusLimit") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("1")>  _
        Public Property Yppmm_Denominator() As Long
            Get
                Return CType(Me("Yppmm_Denominator"),Long)
            End Get
            Set
                Me("Yppmm_Denominator") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0.5")>  _
        Public Property YJogDt() As Double
            Get
                Return CType(Me("YJogDt"),Double)
            End Get
            Set
                Me("YJogDt") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("10000")>  _
        Public Property YJogSpeed() As Long
            Get
                Return CType(Me("YJogSpeed"),Long)
            End Get
            Set
                Me("YJogSpeed") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("90")>  _
        Public Property YJogRange() As Double
            Get
                Return CType(Me("YJogRange"),Double)
            End Get
            Set
                Me("YJogRange") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("1")>  _
        Public Property Xppmm_Denominator() As Long
            Get
                Return CType(Me("Xppmm_Denominator"),Long)
            End Get
            Set
                Me("Xppmm_Denominator") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0.5")>  _
        Public Property XJogDt() As Double
            Get
                Return CType(Me("XJogDt"),Double)
            End Get
            Set
                Me("XJogDt") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("10000")>  _
        Public Property XJogSpeed() As Long
            Get
                Return CType(Me("XJogSpeed"),Long)
            End Get
            Set
                Me("XJogSpeed") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("500")>  _
        Public Property XJogRange() As Long
            Get
                Return CType(Me("XJogRange"),Long)
            End Get
            Set
                Me("XJogRange") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("1421")>  _
        Public Property Zppmm_Numerator() As Long
            Get
                Return CType(Me("Zppmm_Numerator"),Long)
            End Get
            Set
                Me("Zppmm_Numerator") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("1")>  _
        Public Property Zppmm_Denominator() As Long
            Get
                Return CType(Me("Zppmm_Denominator"),Long)
            End Get
            Set
                Me("Zppmm_Denominator") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0.5")>  _
        Public Property ZJogDt() As Double
            Get
                Return CType(Me("ZJogDt"),Double)
            End Get
            Set
                Me("ZJogDt") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("90")>  _
        Public Property ZJogRange() As Double
            Get
                Return CType(Me("ZJogRange"),Double)
            End Get
            Set
                Me("ZJogRange") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0.5")>  _
        Public Property AJogDt() As Double
            Get
                Return CType(Me("AJogDt"),Double)
            End Get
            Set
                Me("AJogDt") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("2741")>  _
        Public Property Yppmm_Numerator() As Long
            Get
                Return CType(Me("Yppmm_Numerator"),Long)
            End Get
            Set
                Me("Yppmm_Numerator") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("800")>  _
        Public Property Xppmm_Numerator() As Long
            Get
                Return CType(Me("Xppmm_Numerator"),Long)
            End Get
            Set
                Me("Xppmm_Numerator") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("20000")>  _
        Public Property ZJogSpeed() As Long
            Get
                Return CType(Me("ZJogSpeed"),Long)
            End Get
            Set
                Me("ZJogSpeed") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("250000")>  _
        Public Property Appmm_Numerator() As Long
            Get
                Return CType(Me("Appmm_Numerator"),Long)
            End Get
            Set
                Me("Appmm_Numerator") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("90")>  _
        Public Property Appmm_Denominator() As Long
            Get
                Return CType(Me("Appmm_Denominator"),Long)
            End Get
            Set
                Me("Appmm_Denominator") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("50000")>  _
        Public Property AJogSpeed() As Long
            Get
                Return CType(Me("AJogSpeed"),Long)
            End Get
            Set
                Me("AJogSpeed") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("90")>  _
        Public Property AJogRange() As Double
            Get
                Return CType(Me("AJogRange"),Double)
            End Get
            Set
                Me("AJogRange") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("67893")>  _
        Public Property Lift_10() As String
            Get
                Return CType(Me("Lift_10"),String)
            End Get
            Set
                Me("Lift_10") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("115490")>  _
        Public Property Lift_up() As String
            Get
                Return CType(Me("Lift_up"),String)
            End Get
            Set
                Me("Lift_up") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("960000")>  _
        Public Property Arm_Speed() As String
            Get
                Return CType(Me("Arm_Speed"),String)
            End Get
            Set
                Me("Arm_Speed") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("6300")>  _
        Public Property Arm_ACC() As String
            Get
                Return CType(Me("Arm_ACC"),String)
            End Get
            Set
                Me("Arm_ACC") = value
            End Set
        End Property
        
        <Global.System.Configuration.ApplicationScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("13")>  _
        Public ReadOnly Property Output_Hand() As Integer
            Get
                Return CType(Me("Output_Hand"),Integer)
            End Get
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0")>  _
        Public Property LIFT_Down() As String
            Get
                Return CType(Me("LIFT_Down"),String)
            End Get
            Set
                Me("LIFT_Down") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0")>  _
        Public Property LIFT_SPEED() As String
            Get
                Return CType(Me("LIFT_SPEED"),String)
            End Get
            Set
                Me("LIFT_SPEED") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property LIFT_DOWN_SPEED() As String
            Get
                Return CType(Me("LIFT_DOWN_SPEED"),String)
            End Get
            Set
                Me("LIFT_DOWN_SPEED") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property LIFT_ACC1() As String
            Get
                Return CType(Me("LIFT_ACC1"),String)
            End Get
            Set
                Me("LIFT_ACC1") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property LIFT_ACC2() As String
            Get
                Return CType(Me("LIFT_ACC2"),String)
            End Get
            Set
                Me("LIFT_ACC2") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property ARM_RANGE() As String
            Get
                Return CType(Me("ARM_RANGE"),String)
            End Get
            Set
                Me("ARM_RANGE") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property Time_HandOFF() As String
            Get
                Return CType(Me("Time_HandOFF"),String)
            End Get
            Set
                Me("Time_HandOFF") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property ARM_TO_CAM_RANGE() As String
            Get
                Return CType(Me("ARM_TO_CAM_RANGE"),String)
            End Get
            Set
                Me("ARM_TO_CAM_RANGE") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property HAND_TO_CAM_RANGE() As String
            Get
                Return CType(Me("HAND_TO_CAM_RANGE"),String)
            End Get
            Set
                Me("HAND_TO_CAM_RANGE") = value
            End Set
        End Property
    End Class
End Namespace

Namespace My
    
    <Global.Microsoft.VisualBasic.HideModuleNameAttribute(),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()>  _
    Friend Module MySettingsProperty
        
        <Global.System.ComponentModel.Design.HelpKeywordAttribute("My.Settings")>  _
        Friend ReadOnly Property Settings() As Global.APicker.My.MySettings
            Get
                Return Global.APicker.My.MySettings.Default
            End Get
        End Property
    End Module
End Namespace


Option Strict Off
Option Explicit On
Public Class adt8940a1
    Declare Function adt8940a1_initial Lib "8940A1.dll" () As Integer
    'Initialize card
    '(1) Return >0 means amount of installed adt8940 cards;in case the returning value is 3, the available card numbers shall be 0, 1, and 2;
    '(2) Return =0 means no installation of adt8940 card;
    '(3) Return <0 means no installation of service if the value is -1 or PCI bus failure is the value is -2.

    Declare Function get_lib_version Lib "8940A1.dll" (ByVal cardno As Integer) As Integer
    'Get current library version

    Declare Function get_hardware_ver Lib "8940A1.dll" (ByVal cardno As Integer) As Double
    ' Get current hardware version
    'Here returns are combination of hardware and hardware version number.

    Declare Function set_pulse_mode Lib "8940A1.dll" (ByVal cardno As Integer, ByVal axis As Integer, ByVal value As Integer, ByVal logic As Long, ByVal dir_logic As Long) As Integer
    'Set outputted pulse mode
    'cardno      Card number
    'axis        Axis number (1-4)
    'value       0£ºPulse + Pulse method     1£ºPulse + direction method
    'logic       0£º Positive logic pulse    1£ºNegative logic pulse
    'dir_logic   0£º Positive logic direction input signal   1£ºNegative logic direction input signal
    'Return      0£ºCorrect                  1£º Wrong
    'Default mode: Pulse + direction, with positive logic pulse and positive logic direction input signal

    Declare Function set_limit_mode Lib "8940A1.dll" (ByVal cardno As Integer, ByVal axis As Integer, ByVal v1 As Integer, ByVal v2 As Integer, ByVal dir_logic As Integer) As Integer
    'Set mode of nLMT signal input along positive/ negative direction
    'cardno  Card number
    'axis    Axis number (1-4)
    'v1      0: Apply negative limit     1: Don't apply negative limit
    'v2      0: Apply low level          1: Apply high level
    'logic   0: Apply positive limit     1: Don't apply positive limit
    'Return  0: Correct                  1: Wrong
    'Default mode: Apply positive and negative limits with low level

    Declare Function set_stop0_mode Lib "8940A1.dll" (ByVal cardno As Integer, ByVal axis As Integer, ByVal v As Integer, ByVal logic As Long) As Integer
    'Set mode of stop0 input signal
    'cardno  Card number
    'axis    Axis number (1-4)
    'v       0: Don't apply          1: Apply
    'logic   0: Apply low level      1: Apply high level
    'Return  0: Correct              1: Wrong
    'Default mode: Don 't apply

    Declare Function set_stop1_mode Lib "8940A1.dll" (ByVal cardno As Integer, ByVal axis As Long, ByVal v As Long, ByVal logic As Long) As Integer
    'Set mode of stop1 input signal
    'cardno  Card number
    'axis    Axis number (1-4)
    'v       0: Don't apply          1: Apply
    'logic   0: Apply low level      1: Apply high level
    'Return  0: Correct              1: Wrong
    'Default mode: Don't apply

    Declare Function set_delay_time Lib "8940A1.dll" (ByVal cardno As Integer, ByVal time As Long) As Integer
    'set delay time
    'cardno  Card number
    'time    Delay time
    'Return  0: Correct              1: Wrong
    'Remark: The time unit is 1/8us, with the maximum integer value as its maximum value

    Declare Function set_suddenstop_mode Lib "8940A1.dll" (ByVal cardno As Integer, ByVal v As Integer, ByVal logic As Integer) As Integer
    'Hardware stop
    'cardno  Card number
    'v       0: Apply;               1: Don't apply
    'logical 0: low level;           1: high level
    'Return  0: Correct              1: Wrong
    'Remark: Hardware stop signals are assigned to use the 34 pin at the P3 terminal panel (IN31)

    '//----------------------------------------------------//
    '//          CATEGORY OF MOTION STATUS CHECK           //
    '//----------------------------------------------------//

    Declare Function get_status Lib "8940A1.dll" (ByVal cardno As Integer, ByVal axis As Integer, ByRef value As Long) As Integer
    'Get status of single-axis drive
    'cardno  Card number
    'axis    Axis number (1-4)
    'value   Indicator of drive status
    '                0: Drive completed
    '            Non-0: Drive in process
    'Return  0: Correct              1: Wrong

    Declare Function get_inp_status Lib "8940A1.dll" (ByVal cardno As Integer, ByRef value As Long) As Integer
    'Get status of Interpolation
    'cardno  Card number
    'value   Indicator of i Interpolation:
    '            0: Interpolation completed
    '            1: Interpolation in process
    'Return  0: Correct              1: Wrong

    Declare Function set_acc Lib "8940A1.dll" (ByVal cardno As Integer, ByVal axis As Integer, ByVal add As Long) As Integer
    'Set acceleration
    'cardno  Card number
    'axis    Axis number
    'Range   (1-8000 for hardware version 1and also hardware version 2)
    'Return  0: Correct              1: Wrong

    Declare Function set_startv Lib "8940A1.dll" (ByVal cardno As Integer, ByVal axis As Integer, ByVal startv As Long) As Integer
    'Set starting speed
    'cardno  Card number
    'axis    Axis number
    'value   Range (1-8000 for hardware version 1 and up to 2M for hardware version 2)
    'Return  0: Correct              1: Wrong

    Declare Function set_speed Lib "8940A1.dll" (ByVal cardno As Integer, ByVal axis As Integer, ByVal speed As Long) As Integer
    'Set drive speed
    'cardno  Card number
    'axis    Axis number
    'value   Range (1-8000 for hardware version 1 and up to 2M for hardware version 2)
    'Return  0: Correct              1: Wrong

    Declare Function set_command_pos Lib "8940A1.dll" (ByVal cardno As Integer, ByVal axis As Integer, ByVal value As Long) As Integer
    'set values for the logical position counter
    'cardno  Card number
    'axis    Axis number
    'value   Range (-2147483648~+2147483647)
    'Return  0: Correct              1: Wrong

    Declare Function set_actual_pos Lib "8940A1.dll" (ByVal cardno As Integer, ByVal axis As Integer, ByVal value As Long) As Integer
    'set values for the actual position counter
    'cardno  Card number
    'axis    Axis number
    'value   Range (-2147483648~+2147483647)
    'Return  0: Correct              1: Wrong

    Declare Function get_command_pos Lib "8940A1.dll" (ByVal cardno As Integer, ByVal axis As Integer, ByRef value As Long) As Integer
    'Get logical position of each axis
    'cardno  Card number
    'axis    Axis number
    'value   Indicator of logical position value
    'Return  0: Correct              1: Wrong

    Declare Function get_actual_pos Lib "8940A1.dll" (ByVal cardno As Integer, ByVal axis As Integer, ByRef value As Long) As Integer
    'Get actual position of each axis (i.e., encoder feedback input)
    'cardno  Card number
    'axis    Axis number
    'value   Indicator of actual position value
    'Return  0: Correct              1: Wrong

    Declare Function get_speed Lib "8940A1.dll" (ByVal cardno As Integer, ByVal axis As Integer, ByRef value As Long) As Integer
    'Get current speed
    'cardno  Card number
    'axis    Axis number
    'value   Indicator of current drive speed
    'Return  0: Correct              1: Wrong

    Declare Function get_out Lib "8940A1.dll" (ByVal cardno As Integer, ByVal number As Integer) As Integer
    '*****************************************************
    'Get  Output point status
    'cardno Card number
    'number Output point
    'Return         Output point status,-1:Wrong
    '*****************************************************/

    Declare Function pmove Lib "8940A1.dll" (ByVal cardno As Integer, ByVal axis As Integer, ByVal value As Long) As Integer
    'Single-axis quantitative drive
    'cardno  Card number
    'axis    Axis number
    'pulse   Outputted pulses
    '                >0: move along positive direction
    '                <0: move along negative direction
    '                Range (-268435455~+268435455)

    Declare Function dec_stop Lib "8940A1.dll" (ByVal cardno As Integer, ByVal axis As Integer) As Integer
    'Deceleration stop
    'cradno  Card number
    'axis    Axis number
    'Return  0: Correct              1: Wrong

    Declare Function sudden_stop Lib "8940A1.dll" (ByVal cardno As Integer, ByVal axis As Integer) As Integer
    'Sudden stop
    'cardno Card number
    'axis   Axis number
    'Return 0: Correct               1: Wrong

    Declare Function inp_move2 Lib "8940A1.dll" (ByVal cardno As Integer, ByVal axis1 As Integer, ByVal axis2 As Integer, ByVal pulse1 As Long, ByVal pulse2 As Long) As Long
    '2-axis interpolation
    'cardno          Card number
    'axis1,axis2     Axis number joining interpolation
    'pulse1,pulse2   Relative distance of movemen
    '                Range (-8388608~+8388607)
    'Return          0: Correct              1: Wrong

    Declare Function inp_move3 Lib "8940A1.dll" (ByVal cardno As Integer, ByVal axis1 As Integer, ByVal axis2 As Integer, ByVal axis3 As Integer, ByVal pulse1 As Long, ByVal pulse2 As Long, ByVal pulse3 As Long) As Long
    '3-axis interpolation
    'cardno          Card number
    'axis1,axis2,axis3  Axis number joining interpolation
    'pulse1,pulse2,pulse3    Relative distance of movemen
    '                        Range (-8388608~+8388607)
    'Return          0: Correct              1: Wrong

    Declare Function inp_move4 Lib "8940A1.dll" (ByVal cardno As Integer, ByVal pulse1 As Long, ByVal pulse2 As Long, ByVal pulse3 As Long, ByVal pulse4 As Long) As Long
    '4-axis interpolation
    'cardno Card number
    'pulse1,pulse2,pulse3,pulse4  Relative distance of movement along X-Y-Z-W axis
    '                             Range (-8388608~+8388607)
    'Return  0: Correct              1: Wrong

    Declare Function read_bit Lib "8940A1.dll" (ByVal cardno As Integer, ByVal number As Long) As Integer
    'Read single input point
    'cardno  Card number
    'number  Input point (0-39)
    'Return
    '        0: low Level
    '        1: high Level
    '       -1: error

    'Declare Function write_bit Lib "8940A1.dll" (ByVal cardno As Integer, ByVal number As Long, ByVal value As Long) As Long
    Declare Function write_bit Lib "8940A1.dll" (ByVal cardno As Integer, ByVal number As Integer, ByVal value As Integer) As Integer
    'Output single output point
    'cardno  Card number
    'number  Output point (0-15)
    'value   0: low      1: high
    'Return  0: correct  1: wrong

    Declare Function get_delay_status Lib "8940A1.dll" (ByVal cardno As Integer) As Integer
    'Get delay status
    'cardno  Card number
    'Return  0: delay stop          1: delay in process

    '*********************************************//
    '                Composite drives             //
    '*********************************************//
    Declare Function set_symmetry_speed Lib "8940A1.dll" (ByVal cardno As Integer, ByVal axis As Integer, ByVal lspd As Long, ByVal hspd As Long, ByVal tacc As Double) As Integer
    '*******************************************************
    'Function:
    '          Set the value of symmetric acceleration/deceleration
    'Parameter:
    '          cardno card number
    '          axis axis number(1-4)
    '          lspd Start speed
    '          hspd Driving speed
    '          tacc Acceleration time
    '          Return value 0:correct 1:wrong
    'Notice:
    'This function is composed of the function that sets acceleration/deceleration mode and the
    'functions that set start velocity, driving speed, acceleration and change rate of
    'acceleration/deceleration.
    '*******************************************************

    Declare Function symmetry_relative_move Lib "8940A1.dll" (ByVal cardno As Integer, ByVal axis As Integer, ByVal pulse As Long, ByVal lspd As Long, ByVal hspd As Long, ByVal tacc As Double) As Integer 'ok
    '********************************************************
    'function: Refer to the current position and perform quantitative movement in the symmetrical
    'acceleration/deceleration
    'para:
    '     cardno-card number
    '     axis---axis number
    '     pulse --pulse
    '     lspd--- Low speed
    '     hspd--- High speed
    '     tacc--- Time of acceleration (Unit: sec)
    'return value 0£ºcorrect 1£ºwrong
    '*********************************************************

    Declare Function symmetry_absolute_move Lib "8940A1.dll" (ByVal cardno As Integer, ByVal axis As Integer, ByVal pulse As Long, ByVal lspd As Long, ByVal hspd As Long, ByVal tacc As Double) As Integer
    '*********************************************************
    'function: Refer to the position of zero point and perform quantitative movement in the symmetrical
    'acceleration/deceleration
    'para:
    '     cardno -card number
    '     axis ---axis number
    '     pulse --pulse
    '     lspd --- Low speed
    '     hspd --- High speed
    '     tacc--- Time of acceleration (Unit: sec)
    'return value 0: correct 1: wrong
    '**********************************************************

    Declare Function symmetry_relative_line2 Lib "8940A1.dll" (ByVal cardno As Integer, ByVal axis1 As Integer, ByVal axis2 As Integer, ByVal pulse1 As Long, ByVal pulse2 As Long, ByVal lspd As Long, ByVal hspd As Long, ByVal tacc As Double) As Integer
    '**********************************************************
    'function: Refer to current position and perform linear interpolation in symmetrical
    'acceleration/deceleration
    'para:
    '     cardno-card number
    '     axis1---axis number1
    '     axis2---axis number2
    '     pulse1-- pulse 1
    '     pulse2-- pulse 2
    '     lspd --- Low speed
    '     hspd --- High speed
    '     tacc--- Time of acceleration (Unit: sec)
    'return value 0£ºcorrect 1£ºwrong
    '***********************************************************

    Declare Function symmetry_absolute_line2 Lib "8940A1.dll" (ByVal cardno As Integer, ByVal axis1 As Integer, ByVal axis2 As Integer, ByVal pulse1 As Long, ByVal pulse2 As Long, ByVal lspd As Long, ByVal hspd As Long, ByVal tacc As Double) As Integer
    '***********************************************************
    'function: Refer to the position of zero point and perform linear interpolation in symmetrical
    'acceleration/deceleration
    'para:
    '     cardno-card number
    '     axis1---axis number1
    '     axis2---axis number2
    '     pulse1¡ªpulse of axis 1
    '     pulse2-- pulse of axis 2
    '     lspd --- Low speed
    '     hspd --- High speed
    '     tacc--- Time of acceleration (Unit: sec)
    'return value 0£ºcorrect 1£ºwrong
    '************************************************************/

    Declare Function symmetry_relative_line3 Lib "8940A1.dll" (ByVal cardno As Integer, ByVal axis1 As Integer, ByVal axis2 As Integer, ByVal axis3 As Integer, ByVal pulse1 As Long, ByVal pulse2 As Long, ByVal pulse3 As Long, ByVal lspd As Long, ByVal hspd As Long, ByVal tacc As Double) As Integer
    '************************************************************
    'function: Refer to current position and perform linear interpolation in symmetric
    'acceleration/deceleration
    'para:
    '     cardno-card number
    '     axis1---axis number1
    '     axis2---axis number2
    '     axis3---axis number3
    '     pulse1-- pulse of axis 1
    '     pulse2-- pulse of axis 2
    '     pulse3-- pulse of axis 3
    '     lspd --- Low speed
    '     hspd --- High speed
    '     tacc--- Time of acceleration (Unit: sec)
    'return value 0£ºcorrect 1£ºwrong
    '***************************************************************

    Declare Function symmetry_absolute_line3 Lib "8940A1.dll" (ByVal cardno As Integer, ByVal axis1 As Integer, ByVal axis2 As Integer, ByVal axis3 As Integer, ByVal pulse1 As Long, ByVal pulse2 As Long, ByVal pulse3 As Long, ByVal lspd As Long, ByVal hspd As Long, ByVal tacc As Double) As Integer
    '**************************************************************
    'function: Refer to the position of zero point and perform linear interpolation in symmetric
    'acceleration/deceleration.
    'para:
    '     cardno-card number
    '     axis1---axis number1
    '     axis2---axis number2
    '     axis3---axis number3
    '     pulse1-- pulse of axis 1
    '     pulse2-- pulse of axis 2
    '     pulse3-- pulse of axis 3
    '     lspd --- Low speed
    '     hspd --- High speed
    '     tacc--- Time of acceleration (Unit: sec)
    'return value 0£ºcorrect 1£ºwrong
    '****************************************************************

    Declare Function symmetry_relative_line4 Lib "8940A1.dll" (ByVal cardno As Integer, ByVal pulse1 As Long, ByVal pulse2 As Long, ByVal pulse3 As Long, ByVal pulse4 As Long, ByVal lspd As Long, ByVal hspd As Long, ByVal tacc As Double) As Integer
    'function: Refer to current position and perform linear interpolation in symmetric
    'acceleration/deceleration
    'para:
    '     cardno-card number
    '     pulse1-- pulse of axis 1
    '     pulse2-- pulse of axis 2
    '     pulse3-- pulse of axis 3
    '     pulse4-- pulse of axis 4
    '     lspd --- Low speed
    '     hspd --- High speed
    '     tacc--- Time of acceleration (Unit: sec)
    'return value 0£ºcorrect 1£ºwrong
    '******************************************************

    Declare Function symmetry_absolute_line4 Lib "8940A1.dll" (ByVal cardno As Integer, ByVal pulse1 As Long, ByVal pulse2 As Long, ByVal pulse3 As Long, ByVal pulse4 As Long, ByVal lspd As Long, ByVal hspd As Long, ByVal tacc As Double) As Integer
    'function: Refer to the position of zero point and perform linear interpolation in symmetric
    'acceleration/deceleration.
    'para:
    '     cardno-card number
    '     pulse1-- pulse of axis 1
    '     pulse2-- pulse of axis 2
    '     pulse3-- pulse of axis 3
    '     pulse4-- pulse of axis 4
    '     lspd --- Low speed
    '     hspd --- High speed
    '     tacc--- Time of acceleration (Unit: sec)
    'return value 0£ºcorrect 1£ºwrong
    ''******************************************************


    '//*********************************************//
    '//         external signal drive               //
    '//*********************************************//

    Declare Function manual_pmove Lib "8940A1.dll" (ByVal cardno As Integer, ByVal axis As Integer, ByVal pos As Long) As Integer
    '/* Quantitative drive function of external signal
    'function: Quantitative drive function of external signal
    'para:
    '     cardno card number
    '     axis axis number
    '     pulse pulse
    'Return 0£ºCorrect 1£ºWrong
    'Note: (1) Send out quantitative pulse, but the drive does not start immediately until the external
    '      signal level changes
    '      (2)Ordinary button and handwheel are acceptable.
    '******************************************************************/

    Declare Function manual_continue Lib "8940A1.dll" (ByVal cardno As Integer, ByVal axis As Integer) As Integer
    '/*Continuous drive function of external signal
    'function: Continuous drive function of external signal
    'para:
    '     cardno card number
    '     axis axis number
    'Return 0£ºCorrect 1£ºWrong
    'Note: (1) Send out fixed pulse, but the drive does not start immediately until the level of external
    'signal changes
    '(2) Ordinary button and handwheel are acceptable.
    '******************************************************************/

    Declare Function manual_disable Lib "8940A1.dll" (ByVal cardno As Integer, ByVal axis As Integer) As Integer
    '/* Shut down the enabling of external signal drive
    'function: Shut down the enabling of external signal drive
    'para:
    '     cardno card number
    '     axis axis number
    'Return 0£ºCorrect 1£ºWrong
    '******************************************************************/

    '//*********************************************//
    '//          lock  position                     //
    '//*********************************************//

    Declare Function set_lock_position Lib "8940A1.dll" (ByVal cardno As Integer, ByVal axis As Integer, ByVal mode As Integer, ByVal regi As Integer, ByVal logical As Integer) As Integer
    '/*set lockmode
    'function:lock the logical position and real position for all axis
    'para:
    '     cardno ¡ªcard number
    '     axis   ¡ªreference axis
    '     mode   ¡ªset lock mode   |0:inefficacy         |1:efficiency
    '     regi   ¡ªregister mode   |0:logical position   |1:real position
    '     logical¡ªlevel signal |0: from high to low  |1:from low to high
    'retutrn 0: correct 1: wrong
    'Note: Use IN signal of specific axis as the trigger signal
    '*******************************************************************/

    Declare Function get_lock_status Lib "8940A1.dll" (ByVal cardno As Integer, ByVal axis As Integer, ByRef v As Integer) As Integer
    '/******************** get synchronous action state ***********************
    'function:get synchronous action state
    'para:
    '     cardno    card number
    '     axis      axis number
    '     status¡ª  0|haven't run synchronous
    '               1|run synchronous
    'retutrn 0: correct 1: wrong
    'Note: This function could tell whether the position lock has been executed
    '******************************************************************/

    Declare Function get_lock_position Lib "8940A1.dll" (ByVal cardno As Integer, ByVal axis As Integer, ByRef pos As Long) As Integer
    '/**********************get lock position************************
    'Function: Get the locked position
    'para:
    '     cardno    card number
    '     axis      axis number
    '     pos lock position
    'Return 0£ºCorrect 1£ºWrong
    '******************************************************************/

    Declare Function clr_lock_status Lib "8940A1.dll" (ByVal cardno As Integer, ByVal axis As Integer) As Integer
    '/**********************clean lock position************************
    'Function: Clean the locked position
    'para:
    '     cardno    card number
    '     axis      axis number
    'Return 0£ºCorrect 1£ºWrong
    '******************************************************************/

    '//*********************************************//
    '//               Hardware Cache                //
    '//*********************************************//
    Declare Function fifo_inp_move1 Lib "8940A1.dll" (ByVal cardno As Integer, ByVal axis1 As Integer, ByVal pulse1 As Long, ByVal speed As Long) As Integer
    '/**************************single axis FIFO**************************
    'Function:single axis FIFO
    'para:
    '    cardno      card number
    '    axis1       axis number(1-4)
    '    pulse1      pulses in FIFO buffer
    '    speed       FIFO speed
    'Return 0£ºCorrect 1£ºWrong
    '******************************************************************/

    Declare Function fifo_inp_move2 Lib "8940A1.dll" (ByVal cardno As Integer, ByVal axis1 As Integer, ByVal axis2 As Integer, ByVal pulse1 As Long, ByVal pulse2 As Long, ByVal speed As Long) As Integer
    '/**************************two axes FIFO**************************
    'Function:single axis FIFO
    'para:
    '    cardno      card number
    '    axis1       axis number(1-4)
    '    axis2       axis number(1-4)
    '    pulse1      pulses in FIFO buffer
    '    pulse2      pulses in FIFO buffer
    '    speed       FIFO speed
    'Return 0£ºCorrect 1£ºWrong
    '******************************************************************/

    Declare Function fifo_inp_move3 Lib "8940A1.dll" (ByVal cardno As Integer, ByVal axis1 As Integer, ByVal axis2 As Integer, ByVal axis3 As Integer, ByVal pulse1 As Long, ByVal pulse2 As Long, ByVal pulse3 As Long, ByVal speed As Long) As Integer
    '/**************************three axes FIFO**************************
    'Function:single axis FIFO
    'para:
    '    cardno      card number
    '    axis1       axis number(1-4)
    '    axis2       axis number(1-4)
    '    axis3       axis number(1-4)
    '    pulse1      pulses in FIFO buffer
    '    pulse2      pulses in FIFO buffer
    '    pulse3      pulses in FIFO buffer
    '    speed       FIFO speed
    'Return 0£ºCorrect 1£ºWrong
    '******************************************************************/

    Declare Function fifo_inp_move4 Lib "8940A1.dll" (ByVal cardno As Integer, ByVal pulse1 As Long, ByVal pulse2 As Long, ByVal pulse3 As Long, ByVal pulse4 As Long, ByVal speed As Long) As Integer
    '/**************************three axes FIFO**************************
    'Function:single axis FIFO
    'para:
    '    cardno      card number
    '    axis1       axis number(1-4)
    '    axis2       axis number(1-4)
    '    axis3       axis number(1-4)
    '    axis4       axis number(1-4)
    '    pulse1      pulses in FIFO buffer
    '    pulse2      pulses in FIFO buffer
    '    pulse3      pulses in FIFO buffer
    '    pulse4      pulses in FIFO buffer
    '    speed       FIFO speed
    'Return 0£ºCorrect 1£ºWrong
    '******************************************************************/

    Declare Function reset_fifo Lib "8940A1.dll" (ByVal cardno As Integer) As Integer
    '/**************************reset fifo buffer**************************
    'Function:reset fifo buffer
    'para:
    '    cardno      card number
    'Return 0£ºCorrect 1£ºWrong
    '******************************************************************/

    Declare Function read_fifo_count Lib "8940A1.dll" (ByVal cardno As Integer, ByRef value As Integer) As Integer
    '/**************************read fifo **********************
    'Function:read fifo buffer,count the fifo command havn't been implemented
    'para:
    '    cardno      card number
    'Return 0£ºCorrect 1£ºWrong
    '******************************************************************/

    Declare Function read_fifo_empty Lib "8940A1.dll" (ByVal cardno As Integer) As Integer
    '/**************************read fifo **********************
    'Function:read the fifo buffer, count fifo whether it was a empty buffer
    'para:
    '    cardno      card number
    'Return 0£ºnon-empty 1£ºempty
    '******************************************************************/

    Declare Function read_fifo_full Lib "8940A1.dll" (ByVal cardno As Integer) As Integer
    '/**************************read fifo **********************
    'Function:read the fifo buffer, estimate for that whether the buffer is full
    'para:
    '    cardno      card number
    'Return 0£ºnon-full 1£ºfull
    '******************************************************************/


    'Public Sub MyProc()

    'DoEvents()

End Class

������� ������ �������������� ����.

������� ������ ������� �� ���� ������:

1) �������.
��� �������� ������� ����� ���������� ��������� ��������� ���� testclient.cmd
2) ��������������.
��� �������� �������������� ����� ���������� ��������� ��������� ���� ������� � �������.
� ������ ������������� ���������� WCF: wcfserver.cmd � wcfclient.cmd ��������������.
� ������ ������������� ���������� THRIFT: thriftserver.cmd � thriftclient.cmd ��������������.

===================================

Decision of the problem of finding autocomplete words. 

Decision of the problem consists of two parts: 

1) Basic. 
To check the base part is enough to run a batch file testclient.cmd 

2) Additional. 
To test the additional part is enough to run a batch file server and the client. 
In the case of technology WCF: wcfserver.cmd and wcfclient.cmd respectively. 
In the case of technology THRIFT: thriftserver.cmd and thriftclient.cmd respectively.
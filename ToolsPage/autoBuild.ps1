<# ��һ���� ��ȡ��ǰ·�� #>
$CurrentFolder = Join-Path $PWD -ChildPath "dist"

if(-not (Test-Path $CurrentFolder -PathType Container)){
    Write-Error "��Ŀ¼�²�����dist�ļ���"
    throw
}

# Ǩ���ļ�
scp -r $CurrentFolder root@karry.org.cn:/www/myBlog

$systemctlCommand = "systemctl restart nginx.service"

# ����SSH��������Ĳ���
 
# ʹ��ssh�����SSH���Ӳ�ִ��systemctl���� - ͨ����Կ����Linux
$sshCommand = "ssh root@karry.org.cn `"$systemctlCommand`""
 
# ִ��SSH���������
$result = Invoke-Expression $sshCommand
Write-Output "Nginx���������ɹ���"
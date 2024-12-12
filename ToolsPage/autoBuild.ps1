<# 第一步： 获取当前路径 #>
$CurrentFolder = Join-Path $PWD -ChildPath "dist"

if(-not (Test-Path $CurrentFolder -PathType Container)){
    Write-Error "该目录下不存在dist文件夹"
    throw
}

# 迁移文件
scp -r $CurrentFolder root@karry.org.cn:/www/myBlog

$systemctlCommand = "systemctl restart nginx.service"

# 定义SSH连接所需的参数
 
# 使用ssh命令建立SSH连接并执行systemctl命令 - 通过密钥连接Linux
$sshCommand = "ssh root@karry.org.cn `"$systemctlCommand`""
 
# 执行SSH命令并输出结果
$result = Invoke-Expression $sshCommand
Write-Output "Nginx服务重启成功！"
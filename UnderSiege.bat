:: Turn off SSL Verification
git config --global http.sslVerify false

:: Change your name and email here
git config --global user.name "YOUR NAME"
git config --global user.email "YOUR_EMAIL@mymail.eku.edu"

:: Clone repo
git clone https://code.eku.edu/michael_rawlings6/UnderSiege.git C:\Users\ekustudent\Desktop\UnderSiege

:: Start unity
start /d "C:\Program Files\Unity\Editor" Unity.exe

:: Start new cmd at repo's folder
start cmd.exe /k "cd /d C:\Users\ekustudent\Desktop\UnderSiege"
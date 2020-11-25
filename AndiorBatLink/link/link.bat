rem 后盘的东西映射到前盘 必须删除映射到的盘的路径
junction -d "F:\ABServer\ABServer"
junction -d "F:\MyFrame\Assets\Scripts\core\socket\cs" 
junction -d  "F:\severMyFram\ConsoleApp1\ConsoleApp1\cs"
junction -d  "F:\severMyFram\Prototocs\proto" 

junction "F:\ABServer\ABServer" "F:\work\MyFrame\AssetBundles"
junction "F:\MyFrame\Assets\Scripts\core\socket\cs" "F:\severMyFram\Prototocs\cs"
junction "F:\severMyFram\ConsoleApp1\ConsoleApp1\cs" "F:\severMyFram\Prototocs\cs" 
junction  "F:\severMyFram\Prototocs\proto" "F:\MyFrame\ProtoToLua\proto"
pause

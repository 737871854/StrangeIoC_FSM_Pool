using UnityEngine;
using System.Collections;
using ProtoBuf;
using System.IO;

public class TestProtobuf : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //User user = new User();
        //user.ID = 100;
        //user.Username = "siki";
        //user.Password = "123456";
        //user.Level = 100;
        //user._UserType = User.UserType.Master;

        ////FileStream fs = File.Create(Application.dataPath+"/user.bin");
        ////print(Application.dataPath + "/user.bin");
        ////Serializer.Serialize<User>(fs, user);
        ////fs.Close();
        //using (var fs = File.Create(Application.dataPath + "/user.bin"))
        //{
        //    Serializer.Serialize<User>(fs, user);
        //}

        User user = null;

        using (var fs = File.OpenRead(Application.dataPath + "/user.bin"))
        {
            user = Serializer.Deserialize<User>(fs);
        }
        print(user.ID);
        print(user._UserType);
        print(user.Username);
        print(user.Password);
        print(user.Level);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

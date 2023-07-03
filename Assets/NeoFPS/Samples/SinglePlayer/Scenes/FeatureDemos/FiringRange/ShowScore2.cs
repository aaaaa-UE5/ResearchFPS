
using UnityEngine;
using UnityEngine.UI;

namespace NeoFPS.Samples.SinglePlayer
{
    public class ShowScore2 : MonoBehaviour
    {

        //　メッセージUI
        [SerializeField]
        protected Text messageText;
        //　表示するメッセージ
        [SerializeField]
        [TextArea(1, 20)]
        private string allMessage = "今回はRPGでよく使われるメッセージ表示機能を作りたいと思います。\n"
                + "メッセージが表示されるスピードの調節も可能であり、改行にも対応します。\n"
                + "改善の余地がかなりありますが、               最低限の機能は備えていると思われます。\n"
                + "ぜひ活用してみてください。\n<>";
        //　使用する分割文字列
        [SerializeField]
        private string splitString = "<>";
        //　分割したメッセージ
        private string[] splitMessage;
        //　分割したメッセージの何番目か
        private int messageNum;
        //　テキストスピード
        [SerializeField]
        private float textSpeed = 0.05f;
        //　経過時間
        private float elapsedTime = 0f;
        //　今見ている文字番号
        private int nowTextNum = 0;
        //　マウスクリックを促すアイコン
        private Image clickIcon;
        //　クリックアイコンの点滅秒数
        [SerializeField]
        private float clickFlashTime = 0.2f;
        //　1回分のメッセージを表示したかどうか
        private bool isOneMessage = false;
        //　メッセージをすべて表示したかどうか
        private bool isEndMessage = false;



        public virtual void finish_traing()
        {

        }

        void Start()
        {
            //clickIcon = transform.Find("Panel/Image").GetComponent<Image>();
            //clickIcon.enabled = false;
            //messageText = GetComponentInChildren<Text>();
            messageText.text = "";
        }

        protected virtual void Update()
        {
            //　メッセージが終わっているか、メッセージがない場合はこれ以降何もしない
            if (isEndMessage || allMessage == null)
            {

                return;
            }


            //　1回に表示するメッセージを表示していない	
            if (!isOneMessage)
            {
                //　テキスト表示時間を経過したらメッセージを追加
                if (elapsedTime >= textSpeed)
                {
                    try
                    {
                        messageText.text += splitMessage[messageNum][nowTextNum];
                    }
                    catch
                    {

                    }
                    

                    nowTextNum++;
                    elapsedTime = 0f;
                }
                elapsedTime += Time.deltaTime;

                //　Enterオされていたらゲームオブジェクト自体の削除
                if (Input.GetKey(KeyCode.Return))
                {
                    isEndMessage = true;
                    transform.GetChild(0).gameObject.SetActive(false);
                    Debug.Log("a1");
                }
            }
            else
            {

                elapsedTime += Time.deltaTime;

                //　マウスクリックされたら次の文字表示処理
                if (Input.GetMouseButtonDown(0))
                {
                    nowTextNum = 0;
                    messageNum++;
                    messageText.text = "";
                    clickIcon.enabled = false;
                    elapsedTime = 0f;
                    isOneMessage = false;

                }

                //　メッセージが全部表示されていたらゲームオブジェクト自体の削除
                if (Input.GetKey(KeyCode.Return))
                {
                    isEndMessage = true;
                    transform.GetChild(0).gameObject.SetActive(false);
                }
            }
        }
        //　新しいメッセージを設定

        //　他のスクリプトから新しいメッセージを設定しUIをアクティブにする
        public virtual void SetMessagePanel(string message)
        {
            //SetMessage(message);
            Debug.Log("scoreshow");
            messageText.text = message;
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}



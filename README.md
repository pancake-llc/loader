# How To Install

Add the lines below to `Packages/manifest.json`

- for version `1.0.1`
```csharp
    "com.pancake.loader": "https://github.com/pancake-llc/loader.git?path=Assets/_Root#1.0.1",
    "com.cysharp.unitask": "https://github.com/Cysharp/UniTask.git?path=src/UniTask/Assets/Plugins/UniTask#2.3.1",
    "com.pancake.common": "https://github.com/pancake-llc/common.git?path=Assets/_Root#1.2.5",
```

# Usages

![image](https://user-images.githubusercontent.com/44673303/179962642-6b8eaea8-f4d3-4ab5-8834-d134aa492cf3.png)

You will mainly use the LoadingSceneManager class.

As you can see in the photo above. 
You will need to select the loading template, 
it is a prefab, it needs to be marked as Addressable and labeled as `loader`

![image](https://user-images.githubusercontent.com/44673303/179961387-0f6c7730-a058-4103-ba53-2eddbc80e52b.png)

then you can then select them via the `Selected Template` property in the `LoadingSceneManager`


`LoadingSceneManager` provides two loading methods:
```csharp
        /// <summary>
        /// <see cref="funcWaiting"/> and <see cref="prepareActiveScene"/> only use for fakeloading
        /// </summary>
        /// <param name="sceneName"></param>
        /// <param name="funcWaiting"> condition to waiting done loading progress</param>
        /// <param name="prepareActiveScene">action prepare call before action scene</param>
        public void LoadScene(string sceneName, Func<bool> funcWaiting = null, Action prepareActiveScene = null)


        /// <summary>
        /// <see cref="funcWaiting"/> and <see cref="prepareActiveScene"/> only use for fakeloading
        /// </summary>
        /// <param name="sceneName"></param>
        /// <param name="subScene"></param>
        /// <param name="funcWaiting"> condition to waiting done loading progress</param>
        /// <param name="prepareActiveScene">action prepare call before action scene</param>
        public void LoadScene(string sceneName, string subScene, Func<bool> funcWaiting = null, Action prepareActiveScene = null)
```

Ex:
```csharp
    private LoadingScreenManager _loading;

    private void Awake()
    {
        _loading = GetComponent<LoadingScreenManager>();
    }
    public void LoadGameplay()
    {
        _loading.LoadScene("gameplay");
    }
    
    public void LoadMenu()
    {
        _loading.LoadScene("menu");
    }
```
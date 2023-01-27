using System;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    #region Порядок событий UNITY

    /*
     Порядок выполнения функций событий
    В Unity3D, существует целый ряд событий, выполняемых в определенном порядке. Этот порядок мы опишем ниже:

    Первая загрузка сцены
    Эти функции вызываются, когда сцена стартует (по одному разу для каждого объекта в кадре).
    Awake: Эта функция всегда вызывается до начала любых функций, а также сразу после инициализации префаба.
    OnEnable: (вызывается, если объект является активным): Эта функция вызывается только после того, как объект будет включен.

    До первого обновления кадров
    Start: вызывается перед прорисовкой первого фрейма, только если сценарий определён.

    В промежутке между кадрами
    OnApplicationPause: Это событие вызывается в конце кадра, когда обнаружена пауза, фактически между обычными обновлениями кадров. После OnApplicationPause прорисовывается один дополнительный кадр для того, чтобы показать окно, которое отображается во время паузы.

    Порядок обновления
    Для отслеживания логики игры, взаимодействия и анимации объектов, положения камеры и т.д., есть несколько различных событий, которые Вы можете использовать. Общий механизм для выполнения большинства задач находится в функции Update(), но есть и другие функции.
    FixedUpdate: FixedUpdate() не зависит от Update(), и может вызываться как чаще него так и реже (обычно вызывается реже, если FPS достаточно высок). Это событие может быть вызвано несколько раз в кадре, если FPS низкий а может быть и вообще не вызвано между кадрами, если FPS высокий. Все физические расчеты движка и обновление происходит сразу после FixedUpdate(). При применении расчетов движения внутри FixedUpdate(), вам не нужно умножать ваше значение на Time.deltaTime. Это потому, что FixedUpdate() вызывается из таймера, независимого от частоты кадров.
    Update: Update() вызывается один раз за кадр. Это основное событие для прорисовки кадра.
    LateUpdate: LateUpdate() вызывается один раз в кадре, после завершения Update(). Любые расчеты, которые осуществляются в Update() будет завершены, при вызове LateUpdate(). Основным использованием LateUpdate() обычно является слежение за камерой от третьего лица. Если Вы осуществите движение Вашего персонажа в событии Update(), то движения камеры и расчётов её месторасположения можете вести в событии LateUpdate(). Это будет гарантировать, что персонаж прошел полностью перед камерой, и закрепил свое расположение.


    Отрисовка сцены (Rendering)
    OnPreCull: Вызывается перед сборкой сцены на камере. Сборка определяет, какие объекты видны камере. OnPreCull вызывается, только если будет происходить «обрезка» сцены от невидимых объектов.
    OnBecameVisible / OnBecameInvisible: Вызывается, когда объект становится видимым / невидимым для любой камеры.
    OnWillRenderObject: Вызывается один раз для каждой камеры, если объект является видимым.
    OnPreRender: Вызывается перед тем, как на камеру начинается отрисовка сцены
    OnRenderObject: Вызывается, когда все объекты сцены прорисованы. Вы можете использовать функции GL или Graphics.DrawMeshNow, что-бы создать свои рисунки на этой камере.
    OnPostRender: Вызывается после завершения отрисовки сцены на камере.
    OnRenderImage (только Pro версия): Вызывается после прорисовки сцены, для постобработки изображения на экране.
    OnGUI: вызывается несколько раз в кадре в ответ на события интерфейса. События расположения и заполнения цветом обрабатываются в первую очередь, а затем события ввода с клавиатуры / мыши.
    OnDrawGizmos: Используется для рисования Gizmo на сцене.

    Сопрограммы
    Обычно вызов сопрограммы выполняется после возвращения функции Update(). Сопрограмма это функция, которая может приостановить исполнение (yield), пока не будет выполнена. Различные виды использования Сопрограмм:
    yield: сопрограмма будет продолжена после всех функций Update(), которые будут вызваны в следующем кадре.
    yield WaitForSeconds(2): Продолжить после указанного времени задержки, когда все функции Update() уже были вызваны в кадре
    yield WaitForFixedUpdate(): Продолжается, когда все функции FixedUpdate() уже были вызваны
    yield WWW: Продолжается, когда загрузка WWW-контента завершена.
    yield StartCoroutine(MyFunc): Связи сопрограмм, вызов сопрограммы будет ожидать завершения функции MyFunc.

    Разрушение объектов
    OnDestroy: Эта функция вызывается для последнего кадра существования объекта (объект может быть уничтожен в ответ на Object.Destroy или при закрытии сцены).

    При выходе
    Эти функции вызываются для всех активных объектов в сцене:
    OnApplicationQuit: Эта функция вызывается для всех игровых объектов перед закрытием приложения. В редакторе это происходит, когда пользователь прекращает PlayMode. В веб-плеер это происходит при закрытии веб-плеера.
    OnDisable: Эта функция вызывается, когда объект отключается или становится неактивным.

    Таким образом, происходит такой порядок выполнения скриптов:
    Все события Awake
    Все события Start
    цикл (с шагом в переменной delta time)
    Все функции FixedUpdate
    отработка физического движка
    события триггеров OnEnter/Exit/Stay
    события столкновений OnEnter/Exit/Stay
    Rigidbody преобразования, согласно transform.position и вращения
    OnMouseDown/OnMouseUp др. события ввода
    Все события Update()
    Анимация, смешение и трансформация
    Все события LateUpdate
    Прорисовка (Rendering)

    Советы
    Если Вы запускаете сопрограммы в LateUpdate, то они также будут вызваны после LateUpdate непосредственно перед рендерингом.
    Сопрограммы выполняются после всех функций Update().
    */

    #endregion

    /*
     
       Создайте и развивайте ощущение исследования.
       Рассмотрите варианты активной и пассивной игры (а в идеале используйте оба варианта).
       Не пренебрегайте темой и графическим стилем.
       Создайте экспоненциальное масштабирование цен. Наиболее часто используется форма , где Multiplier имеет значение от  до .
       Предоставьте игроку различные способы оптимизации.
       Увеличьте время игрового процесса, стратегически воспользовавшись сбросом и механиками увеличения сложности.

       Price = BaseCost * Multiplayer ^ level

       В нашем примере с Treebeard базовая цена равна 50, переменная «Multiplier» — 1,07, 
       поэтому цены второго уровня равны , третьего уровня  и так далее. 
       Значение Multiplier определяет кривизну линии: чем выше значения, 
       тем круче кривые цен. (При значении 1 мы получим линейную кривую цены.)

       В Clicker Heroes используется 1,07 как множитель прироста для всех 35 улучшаемых героев, 
       а для всех зданий Cookie Clicker используется значение 1,15. Интересно, 
       что у всех бизнесов в AdVenture Capitalist используются разные множители, 
       но все они находятся в интервале от 1,07 до 1,15. 
       Частое использование одинакового множителя в разных играх означает, 
       что получаемые в этих границах кривые сбалансированы и удобны.

       formula:

       prestige = (150 * Math.Sqrt(GOLD / 1e15)) + 1;

   */

    public static DebugPanel DebugPanel;
    public static CurrencyManager CurrencyManager;
    public static UI_Manager UI_Manager;
    public static SaveLoadManager SaveLoadManager;
    public static BuildingManager BuildingManager;
    public static ClickManager ClickManager;
    public static ConfigManager ConfigManager;
    public static ShopManager ShopManager;
    public static AchievementManager AchievementManager;
    public static MessageQueueManager MessageQueueManager;

    private async void Start()
    {
        MessageQueueManager.Init();
        CurrencyManager.Init();
        UI_Manager.Init();
        await SaveLoadManager.Init();
        ConfigManager.Init();
        ShopManager.Init();
        ClickManager.Init();
        BuildingManager.Init();
        AchievementManager.Init();
        
        UI_Manager.Subscribe();

        SaveLoadManager.LoadUIBuyCount();
        SaveLoadManager.LoadCurrency();
        SaveLoadManager.LoadAchievements();
        SaveLoadManager.LoadBuildings();
        BuildingManager.FillList();
        
        foreach (var building in BuildingManager.BuildingsList)
        {
            BuildingManager.FillUpdateList(building.GetBuildingUpdates());
        }

        ClickManager.Subscribe();
        AchievementManager.Subscribe();

        SaveLoadManager.LoadOfflineTime();
        
        BuildingManager.StartTick();
        
        StartCoroutine(UI_Manager.RemoveLoader(2));
    }

    void Update()
    {

        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Home) || Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Menu))
            {
                ExitGame();
            }
        }
        
        if (Input.GetKey(KeyCode.Escape))
        {
            UI_Manager.ShowQuestionPopup("Are you sure you want to exit?", "Yes", "I'm stay", () =>
            {
                ExitGame();
            }, null);
        }

    }

    public static async void ResetGame()
    {
        BuildingManager.Reset();
        AchievementManager.Reset();
        ClickManager.Reset();
        CurrencyManager.Reset();
        SaveLoadManager.Reset();
        
        await SaveLoadManager.AsyncSave();
    }

    private void OnApplicationQuit()
    {
        ExitGame();
    }

    private static async void ExitGame()
    {
        await SaveLoadManager.AsyncSave();

        Application.Quit();
    }

}

using System;
using System.Collections.Generic;
using WpfKeyboardSimulatorApp.model;
using static System.Net.WebRequestMethods;

namespace WpfKeyboardSimulatorApp.repository
{
    //Это фрагмент кода для интерфейса и класса, который реализует этот интерфейс
    //Класс хранилища словарей реализует интерфейс хранилища IDictionary
    //Метод Find By Level в классе хранилища словарей принимает объект класса Level в качестве аргумента и
    //возвращает список строк, соответствующих указанному уровню сложности. Метод просматривает список строк
    //в поле _dictionary, используя объект Level в качестве ключа.
    public interface IDictionaryRepository
    {
        List<String> FindByLevel(Level level);
    }
  
    //
    class DictionaryRepository : IDictionaryRepository
    {
        private Dictionary<Level, List<string>> _dictionary;

        public DictionaryRepository()
        {
            _dictionary = new Dictionary<Level, List<string>>();
            _dictionary[Level.Beginner] = new List<string>();
            _dictionary[Level.Beginner].Add("Картечь хватила в самую середину толпы.");
            _dictionary[Level.Beginner].Add("Сегодня идет сильный дождь!");
            _dictionary[Level.Beginner].Add("Уймись, или худо будет.");
            _dictionary[Level.Beginner].Add("Солнечные чудесные города.");
            _dictionary[Level.Beginner].Add("Однажды Петя возвращался из детского сада.");


            _dictionary[Level.Elementary] = new List<string>();
            _dictionary[Level.Elementary].Add("Башковитый, живописный, легкоатлет, отчетность, решетчатый, туманиться.");
            _dictionary[Level.Elementary].Add("Побрякушка, технология, фальцовщик, акварелист, возглавить, желторотый.");
            _dictionary[Level.Elementary].Add("Напряжение, увлекаться, рукописный, оглавление, верёвочный, акробатика.");
            _dictionary[Level.Elementary].Add("Купальщица, душевность, ободряющий, назойливый, заваривать, изуверство.");
            _dictionary[Level.Elementary].Add("Безмолвный, надобность, милостивый, малодушный, ныряльщица, отмеченный.");
            _dictionary[Level.Elementary].Add("Каракатица, желудочный, кавалерист, педагогика, победитель, землепашец.");
           
            _dictionary[Level.PreIntermediate] = new List<string>();
            _dictionary[Level.PreIntermediate].Add("Жизнь дается один раз, и хочется прожить ее бодро, осмысленно, красиво.");
            _dictionary[Level.PreIntermediate].Add("Он охотно давал их читать, никогда не требуя их назад; зато никогда не возвращал хозяину книги.");
            _dictionary[Level.PreIntermediate].Add("Канонада стала слабее, однако трескотня ружей сзади и справа слышалась все чаще и чаще.");
            _dictionary[Level.PreIntermediate].Add("То степь открывалась далекая и молчаливая, то низкие, подернутые кровью тучи тонули в черном мраке.");
            _dictionary[Level.PreIntermediate].Add("Время стояло самое благоприятное, то есть было темно, слегка морозно и совершенно тихо.");
            _dictionary[Level.PreIntermediate].Add("До этой поры он не жил, а лишь существовал, правда очень недурно, но все же возлагая все надежды на будущее.");

            
            _dictionary[Level.Intermediate] = new List<string>();
            _dictionary[Level.Intermediate].Add("Электроаппаратостроение, райпромторгпродтех, термогидродинамика, трансформируемый, регенерирующее.");
            _dictionary[Level.Intermediate].Add("Разрекламированный, фантасмагорический, абстрагирование, идиосинкразия, конгруэнтность, консерватор.");
            _dictionary[Level.Intermediate].Add("Оксюморон, консенсус, гештальт, ортодокс, трюизм, парадигма, паноптикум, гильотинировать, инкриминировать.");
            _dictionary[Level.Intermediate].Add("Эскалация, фрустрация, сублимация, прекариат, мачизм, рекогносцировка, снобизм, фасилитация, эпикурейство.");
            _dictionary[Level.Intermediate].Add("Крепостничество, меланходический, ректифицировать, тифлопедагогика, ходатайствовать, языкотворчество. ");
           
            _dictionary[Level.Advanced] = new List<string>();
            _dictionary[Level.Advanced].Add("Я напрасно сижу в пальто и кепке, с не закуренной трубкой во рту; я не могу представить себе, как здесь жили, может быть, и лучше, чем я.");
            _dictionary[Level.Advanced].Add("Обе казались спокойны и смелы; однако ж при моем приближении обе потупили голову и закрылись своими изодранными чадрами.");
            _dictionary[Level.Advanced].Add("Молодой человек взял их и до того рассердился, что хотел было уже уйти; но тотчас одумался, вспомнив, что идти больше некуда. ");
            _dictionary[Level.Advanced].Add("Этот немощный безумный старик наконец понял, что даже демону поставлен предел в его способности творить зло.");
            _dictionary[Level.Advanced].Add("C утра до утра шел не переставая мелкий, как водяная пыль, дождик, превращавший глинистые дороги, в которой увязали надолго экипажи.");
            
            _dictionary[Level.Fluent] = new List<string>();
            _dictionary[Level.Fluent].Add("Кокамидопропилпропиленгликольдимонийхлоридфосфат, фиброэзофагогастродуоденоскопия.");
            _dictionary[Level.Fluent].Add("Тетрагидропиранилциклопентилтетрагидропиридопиридиновые, метоксихлордиэтиламинометилбутиламиноакридин.");
            _dictionary[Level.Fluent].Add("Гидразинокарбонилметилбромфенилдигидробенздиазепин, рентгеноэлектрокардиографического.");
            _dictionary[Level.Fluent].Add("Диметилалкилбензиламмонийхлорид, превысокомногорассмотрительствующий");
            _dictionary[Level.Fluent].Add("Метилпропенилендигидроксициннаменилакрилическая кислота, тринитротолуол");
           

        }

        //Этот метод ищет слова в словаре по заданному уровню сложности и возвращает список найденных слов.
        public List<string> FindByLevel(Level level)
        {
            return _dictionary.GetValueOrDefault(level);
        }
    }
}
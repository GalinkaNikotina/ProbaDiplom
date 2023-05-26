using System;
using System.Collections.Generic;
using WpfKeyboardSimulatorApp.model;

namespace WpfKeyboardSimulatorApp.repository
{
    public interface IDictionaryRepository
    {
        List<String> FindByLevel(Level level);
    }

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
            _dictionary[Level.Elementary].Add("В сенях пах­ло све­жи­ми ябло­ка­ми и висе­ли вол­чьи и лисьи шкуры.");
            _dictionary[Level.Elementary].Add("Ни души не попа­лось на пути, ни огня за став­ня­ми.");
            _dictionary[Level.Elementary].Add("Казнить так казнить, жаловать так жаловать");
            _dictionary[Level.Elementary].Add("То ль былое вспо­ми­на­ет­ся, то ль небыв­шее зовёт?");
            _dictionary[Level.Elementary].Add("Тропинка обо­гну­ла куст ореш­ни­ка, и лес сра­зу раз­дал­ся в сто­ро­ны.");
            _dictionary[Level.Elementary].Add("Голова еще боле­ла, созна­ние же было ясное, отчёт­ли­вое.");
           
            _dictionary[Level.PreIntermediate] = new List<string>();
            _dictionary[Level.PreIntermediate].Add("Жизнь дается один раз, и хочется прожить ее бодро, осмысленно, красиво.");
            _dictionary[Level.PreIntermediate].Add("Он охотно давал их читать, никогда не требуя их назад; зато никогда не возвращал хозяину книги, им занятой.");
            _dictionary[Level.PreIntermediate].Add("Канонада ста­ла сла­бее, одна­ко трес­кот­ня ружей сза­ди и спра­ва слы­ша­лась все чаще и чаще.");
            _dictionary[Level.PreIntermediate].Add("То степь откры­ва­лась далё­кая и мол­ча­ли­вая, то низ­кие, подер­ну­тые кро­вью тучи, " +
                                                   "а то и люди, и паро­вик, и моло­тил­ка разом тону­ли в чер­ном мра­ке.");
            _dictionary[Level.PreIntermediate].Add("Время стояло самое благоприятное, то есть было темно, слегка морозно и совершенно тихо.");
            _dictionary[Level.PreIntermediate].Add("До этой поры он не жил, а лишь существовал, правда очень недурно, но все же возлагая все надежды на будущее.");

            
            _dictionary[Level.Intermediate] = new List<string>();
            _dictionary[Level.Intermediate].Add("Светлые, прозрачно набегающие морщины моют золотой песок, да чуть приметно " +
                                                "шевелятся тёмные листья задумчиво свесившихся над обрывом с размытыми весеннею водою корнями деревьев.");
            _dictionary[Level.Intermediate].Add("Милостивое писание ваше я получил, в котором изволишь гневаться на меня, раба вашего, что-де стыдно мне не исполнять господских приказаний; а я, не старый пес, а верный ваш слуга, господских " +
                                                "приказаний слушаюсь и усердно вам всегда служил и дожил до седых волос");
            _dictionary[Level.Intermediate].Add("Я хотел уже выйти из дому, как дверь моя отворилась и ко мне явился капрал с донесением, " +
                                                "что наши казаки ночью выступили из крепости, взяв насильно с собою Юлая, и что около крепости разъезжают неведомые люди.");
            _dictionary[Level.Intermediate].Add("Туман лежал белой колыхающейся гладью у его ног, но над ним сияло голубое небо, " +
                                                "и шептались душистые зеленые ветви, а золотые лучи солнца звенели ликующим торжеством победы.");
            _dictionary[Level.Intermediate].Add("Ней, шедший последним (потому что, несмотря на несчастное их положение или именно вследствие его, им хотелось побить тот пол, который ушиб их, он занялся взрыванием никому не мешавших стен Смоленска), — " +
                                                "шедший последним, Ней, с своим десятитысячным корпусом, прибежал в Оршу к " +
                                                "Наполеону только с тысячью человеками, побросав и всех людей, и все пушки и ночью, украдучись, пробравшись лесом через Днепр.");
        }

        public List<string> FindByLevel(Level level)
        {
            return _dictionary.GetValueOrDefault(level);
        }
    }
}
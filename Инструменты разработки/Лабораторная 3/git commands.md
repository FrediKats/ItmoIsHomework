# Create, setup, config
- config (конфигурация гита: имя пользователя, текстовый редактор)
    - --**replace-all**
    - --add (добавляет строку настройки без изменения уже существующих настроек)
    - --get (получение значения по ключу)
    - --get-all (получение всех пар ключ-значение)
    - --get-regexp (фильтр по регуляркам)
    - Ключ для установки настроек:
        - --global (установка настроек глобально)
        - --**system**
        - --local (установка настроек в локальном репозитории)
    - --list (показать все настройки)
    - -l/--list (список всех настроек в конфиг-файле)
    - --**type**
    - -z/--null (заканчивать вывод информации нул-чаром вместо переноса строки)
    - --name-only (вывод только названий переменных из конфига для команд --list)
    - --**show-origin**
    - -e/--edit (открывает редактор для редактирования конфиг-файла)
- init: (создает пустой git-репозиторий или пересоздает поверх существующего )
    - -q/-quite (Выводить только ошибки и предупреждения, игнорируя остальной вывод)
    - --bare
    -
    -
- clone (клонирование репозитория в новую папку)

# Snapshot
- add (добавить файл под версионный контроль)
    - --dry-run
    - --verbose
    - --force (игнорирование остальных файлов)
    - --interactive
    - --patch
    - --edit
    - --update
    - --all/--no-ignore-removal
    - --no-all/--ignore-removal
    - --refresh
    - --ignore-errors
    - --ignore-missing
    - --no-warn-embedded-repo
    - --renormalize
    - --chmod=(+|-)x (установка прав на файл)
- status (показывает статус рабочего дерева и состояния файлов)
- diff (показывает разницу между коммитами/ветками)
    - --patch (генерирует патч) 
    - --no-patch
    - --unified=<n> (показывает diff с n-ым кол-вом строк) 
    - --raw
    - --patch-with-raw
    - --indent-heuristic
    - --no-indent-heuristic
    - --minimal
    - --patience
    - --histogram
    - --anchored=<text>
    - --diff-algorithm={patience|minimal|histogram|myers} (выбирает нужный алгоритм)
    - --stat[=<width>[,<name-width>[,<count>]]]
    - --compact-summary
    - --numstat
    - --shortstat
    - --dirstat[=<param1,param2,…​>]
    - --summary
    - --patch-with-stat
    - -z
    - --name-only
    - --name-status
    - --submodule[=<format>]
    - --color[=<when>]
    - --no-color
    - --color-moved[=<mode>]
    - --word-diff[=<mode>]
    - --word-diff-regex=<regex>
    - --color-words[=<regex>]
    - --no-renames
    - --check
    - --ws-error-highlight=<kind>
    - --full-index
    - --binary
    - --abbrev[=<n>]
    - --break-rewrites[=[<n>][/<m>]]
    - --find-renames[=<n>]
    - --find-copies[=<n>]
    - -find-copies-harder
      
      бля нахуй это всё
    - --cached
    - --no-index

- commit (создание коммита т.е. фиксация некого состояние репозитория)
- **reset**
- rm (удалить файл из версионного контроль)
    - --force
    - --dry-run
    - -r(рекурсивное удаление)
    - --cached
    - --ignore-unmatch
    - --quiet (без вывода логов)

- rebase (перемещение коммитов из ветки на последний коммит другой ветки)
    - --onto <branchname> (ветка, на которую перемещаем)

# Branches
- branch (выполнение различных операций с ветками)
- checkout (создание ветки и установка HEAD на неё)
    - --quiet
    - --force (выбрасывает изменения и выполняет переход)
    - --ours/ --theirs
    - -b <new_branch> (создание новой ветки)
    - --track
    - --no-track
    - -l
    - --detach
    - --orphan <new_branch>
    - --merge
    - --conflict=<style>
    - --patch
    - --ignore-other-worktrees
    - --[no-]recurse-submodules


- merge (слияние веток)
- stash (scm: стешит изменения в грязную рабочую директорию) (от себя: сохранение изменений локально без комита)
- tag (создание, удаление и проверка тега объекта)

# Sharing
- fetch (скачивание объектов и ссылок из другого репозитория)
- pull (выполнение fetch и интеграция с другим репозиторием или локальной веткой)
- push (обновление удалённого репозитория)
- remote (управление репозиториями)

# Inspection
- show (отображает различные типы объектов)
- log (вывод лога коммитов)
    - --graph (вывод дерева коммитов в виде графа)   
    - 
# Administration       
- fsck: проверяет объекты в базе данных
    - --unreachable: вывод всех объектов, которые существуют, но не доступны ни с одной ноды (reference nodes)
    - --[no-]dangling: вывод всех объектов, которые существуют, но никогда не используются напрямую
    - --**root**: TODO: ?? (Report root nodes.)
    - --**tags**: TODO: ?? (Report tags.)
    - --cache: 
- gc (удаление ненужных файлов)
    - --aggressive
    - --auto
    - --prune=<date>
    - --no-prune
    - --quiet
    - --force
    - --keep-largest-pack
- instaweb (встроенная утилита, для просмотра состояния репозитория в браузере)
- archive (создание архива файлов из указанного дерева)
- prune (удаляет все недостижимые объекты с базы данных объектов)
- 
# Plumbing Commands (а я бы назвал ватзефак-команды)
- cat-file (контент, тип и информация о размере объектов репозитория)
- ls-tree (список элементов дерева объектов)
- grep (вывод строк, подходящих под указанный паттерн)

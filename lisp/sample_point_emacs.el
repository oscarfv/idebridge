;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
;;;;;;;;;;;;; below setup of csharp-mode and ide-bridge :

(autoload 'csharp-mode "csharp-mode" "Major mode for editing C# code." t)
 (setq auto-mode-alist
   (append '(("\\.cs$" . csharp-mode)) auto-mode-alist))

(load-library "dino-csharp")

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
;;;;;;;;;;;;; below compilation font-lock if you want color in compilation output :

(setq compilation-mode-font-lock-keywords
      (append compilation-mode-font-lock-keywords
              '(("duration: \\([0-9]+\\.[0-9]+\\)"
                 (1 font-lock-keyword-face))
                ("\\(\-\-\-\-\-\- Build started\:\\)"
                 (1 font-lock-builtin-face))
                ("exe \\(build [a-z0-9A-Z\\.]*\\)"
                 (1 font-lock-keyword-face))
                ("\\([a-z0-9A-Z\\.]*\\) *\-\>"
                 (1 font-lock-keyword-face))
                ("Compiling \\([a-z0-9A-Z\\.]*\\)"
                 (1 font-lock-keyword-face))
                ("\\(------ Skipped Build: Project:\\)"
                 (1 font-lock-comment-face))
                ("------ Skipped Build: Project: \\([a-zA-Z0-9]*\\)"
                 (1 font-lock-keyword-face))
                (".*\-\> \\(compiling\\)"
                 (1 font-lock-constant-face))
                ("\\(Build finished successfully.\\)"
                 (1 compilation-info-face))
                ("\\(Build failed.\\)"
                 (1 compilation-warning-face))
                ("\\(Linking :\\)"
                 (1 font-lock-keyword-face)))))


;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
;;;;;;;;;;;;; below compilation regexp if needed :

;; to allow next-error to work with csc.exe:
(setq compilation-scroll-output t)
(setq-default compilation-error-regexp-alist
;       (append
 '(
 ; Microsoft VJC:
 ;sample.java(6,1) : error J0020: Expected 'class' or 'interface'
 ("\\(\\([a-zA-Z]:\\)?[^:(\t\n]+\\)(\\([0-9]+\\)[,]\\([0-9]+\\)) ?: \\([eE]rror\\|[Ww]arning\\) VJS[0-9]+:" 1 3 4)

 ;; dinoch - Wed, 04 Aug 2004  09:29
 ;; handle whitespace at beginning of line (for nant output)
 ;;
 ;C# Compiler
 ;t.cs(6,18): error SC1006: Name of constructor must match name of class
 ;
 ("[ \t]*\\(\\([_a-zA-Z:\]:\\)?[^:(\t\n]+\\)(\\([0-9]+\\)[,]\\([0-9]+\\)) ?: \\([Ee]rror\\|[Ww]arning\\) [A-Z]+[0-9]+:" 1 3 4)

 ; Microsoft C/C++:
 ;  keyboard.c(537) : warning C4005: 'min' : macro redefinition
 ;  d:\tmp\test.c(23) : error C2143: syntax error : missing ';' before 'if'
 ;VC EEi
 ;e:\projects\myce40\tok.h(85) : error C2236: unexpected 'class' '$S1'
 ;myc.cpp(14) : error C3149: 'class System::String' : illegal use of managed type 'String'; did you forget a '*'?
    ("\\(\\([a-zA-Z]:\\)?[^:(\t\n]+\\)(\\([0-9]+\\)) \
: \\([eE]rror\\|[Ww]arning\\) C[0-9]+:" 1 3)
 )
;;  compilation-error-regexp-alist)
)

(if nil
    (setq compilation-error-regexp-alist
                                        ;       (append
          '(
                                        ; Microsoft VJC:
                                        ;sample.java(6,1) : error J0020: Expected 'class' or 'interface'
            ("coco.exe \\(\\([a-zA-Z]:\\)?[^:(\t\n]+\\)" 1 nil nil)

            ;; dinoch - Wed, 04 Aug 2004  09:29
            ;; handle whitespace at beginning of line (for nant output)
            ;;
                                        ;C# Compiler
                                        ;t.cs(6,18): error SC1006: Name of constructor must match name of class
                                        ;
            ("-- line \\([0-9]+\\) col \\([0-9]+\\)" nil 1 2)
            ;;  compilation-error-regexp-alist)
            )))

(setq compilation-error-regexp-alist
(append compilation-error-regexp-alist '((".*:in `[A-Za-z \n\t]+': ?\\(\\([a-zA-Z]:\\)?[^:(\t\n]+\\):\\([0-9]+\\):" 1 3))))

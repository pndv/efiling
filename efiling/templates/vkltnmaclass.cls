\NeedsTeXFormat{LaTeX2e}
\ProvidesClass{vakalatnama}[2020/09/15 Vakalatnama template for Indian courts]

\LoadClass[titlepage]{article}

\RequirePackage{xparse}
\RequirePackage[driver=xetex,a4paper,noheadfoot,includeheadfoot,nomarginpar,top=1.5in,bottom=1.5in,left=1.75in,right=1.0in]{geometry}
\RequirePackage{xunicode}

% Set font to Times New Roman
\RequirePackage{fontspec}
\setmainfont{Times New Roman}

% line spacing = 1.5
\RequirePackage[onehalfspacing]{setspace}

\RequirePackage[parfill]{parskip}

% Format section title to have numbers in Table of Contents, but not in document
\newcommand{\secpagebreak}{}
\RequirePackage{titlesec}
\titleformat{\section}{\center \normalfont \bfseries}{}{0pt}{\secpagebreak}

% Format Table of Contents
\RequirePackage{etoc}
\newcommand{\titleoftoc}{\textsc{Index}}
\newcommand{\mktoc}{% Cover page
\begin{center}
    \etocsetstyle{section}
    {}
    {\etociffirst{ \\ \hline}{\\\hline}}
    %should be \etocnumber & \etocname & \etocnumber but I can't get the page numbering correct
    {\etocnumber & \etocname & \etocnumber }
    {}
    \etocsettocstyle{\hypersetup{hidelinks}
    \begin{tabular}{|c|l|c|}
        \multicolumn{3}{c}{\titleoftoc} \\
        \hline
        \multicolumn{1}{|c|}{\bfseries Si No} &
        {\bfseries Particulars} &
        {\bfseries Page}
    }
    {\\ \hline
\end{tabular}}
\etocglobaldefs
\etocsetnexttocdepth{1}
\tableofcontents
\end{center}
}

% Make entries in the index table as links
\RequirePackage[linktocpage=true]{hyperref}

\newcommand{\petitioner}[1]{\newcommand{\@petitioner}{#1}}
\newcommand{\respondent}[1]{\newcommand{\respndt}{#1}}
\newcommand{\court}[1]{\newcommand{\crt}{#1}}
\newcommand{\jurisdiction}[1]{\newcommand{\jurisdctn}{#1}}
\newcommand{\petitiontype}[1]{\newcommand{\ptntype}{#1}}

\NewDocumentCommand {\advname} {O{Shri} m m} { % Title (default=Shri), first, last
    \newcommand {\advtfname} {#1 #2 #3} % Full name with title
    \newcommand {\advfname} {#2 #3} % first + last name
    \newcommand {\advtlname} {#1 #3} % title + last name
}

\NewDocumentCommand {\advname} {O{Shri} m m} { % Title (default=Shri), first, last
    \newcommand {\advtfname} {#1 #2 #3} % Full name with title
    \newcommand {\advfname} {#2 #3} % first + last name
    \newcommand {\advtlname} {#1 #3} % title + last name
}


\newcommand{\advocate}[2][Shri]{
\newcommand{\adv}{#2}
\newcommand{\fulladv}{#1 #2}
}

\NewDocumentCommand{\@addr}{m o o m m m}{  %Line1, Line2, Line3, City, State, Pin
    \newcommand{\@slfulladdr}{ % Single line full address
        \IfNoValueTF{#2}
            { \IfNoValueTF{#3}
                { #1 #4, #5 #6 }
                { #1, #3, #4, #5 #6 }
            }
            { \IfNoValueTF{#3}
                { #1, #2, #4, #5 #6 }
                { #1, #2, #3, #4, #5 #6 }
            }
    }
    \newcommand{\@mlfulladdr}{ % multi-line full address
        \IfNoValueTF{#2}
            { \IfNoValueTF{#3}
                { #1, \\ #4, \\ #5 #6 }
                { #1, \\ #3, \\ #4, \\ #5 #6 }
            }
            { \IfNoValueTF{#3}
                { #1, \\ #2, \\ #4, \\ #5 #6 }
                { #1, \\ #2, \\ #3, \\ #4, \\ #5 #6 }
            }
    }
}


\newcommand{\clientdetails}[1]{\newcommand{\clntdtls}{#1}}

\newcommand{\caseNumbers}[2]{
\newcommand{\fullcasenumber}{#1 of #2}
\newcommand{\shortcasenumber}{#1/#2}
}


\newcommand{\casetitle}{
{\centering
\textsc{In the \crt{}} \\
\textsc{\jurisdctn{}} \\
\textsc{\ptntype{} \fullcasenumber{}} \\
\par}
}

\newcommand{\inmatterof}{%
\begin{tabular}{l c r}
\multicolumn{3}{l}{\underline{\textsc{In the matter of:}}}  \tabularnewline
\tabularnewline
\@petitioner & \ldots & Petitioner \\* %\tabularnewline
\multicolumn{3}{c}{-- \textsc{versus} --} \tabularnewline
\respndt     & \ldots & Respondent \tabularnewline
\end{tabular}
}

\newcommand{\parties}{{\centering
\begin{tabular}{l c r}
\@petitioner & \ldots & Appellant/Petitioner \tabularnewline
\multicolumn{3}{c}{-- \textsc{Between/And} --} \tabularnewline
\respndt & \ldots & Defendant/Respondent/Opposite Party \tabularnewline
\end{tabular}
\par }}

\newcommand {\mktitle} {
% Cover page
\thispagestyle{empty}
\casetitle{}

\inmatterof{}

\vspace*{2em} %\tableofcontents
\mktoc
\vspace*{3em}
\textbf{Advocate for the Respondent: \fulladv{}}

%

% Break after each section
\renewcommand{\secpagebreak}{\clearpage}

%% Set page number and section numbering
\setcounter{section}{0}
\setcounter{page}{0}

}

\ProcessOptions\relax
\endinput

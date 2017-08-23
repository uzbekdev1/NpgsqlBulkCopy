/*
 * This file is part of the Mingw32 package.
 *
 * unistd.h maps (roughly) to io.h
 */

#ifndef __STRICT_ANSI__
# ifndef __MINGW32__
#  include <io.h>
#  include <process.h>

# else
#  include_next <unistd.h>
# endif /* __MINGW32__ */
#endif /* __STRICT_ANSI__ */

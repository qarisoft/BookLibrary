"use client"
import { BackendUrl } from "@/lib/utils";
import Image from "next/image";
import Link from "next/link";
import { useEffect, useState } from "react";
import {
  Drawer,
  DrawerClose,
  DrawerContent,
  DrawerDescription,
  DrawerFooter,
  DrawerHeader,
  DrawerTitle,
  DrawerTrigger,
} from "@/components/ui/drawer"
import { Button } from "@/components/ui/button";
interface Book {
  id: number,
  bookName: string,
  authorName: string,
  price: number,
  image: string | undefined,
  genreId: number,
  genre: string | undefined

}
export default function Home() {
  const [books, setBooks] = useState<Book[]>([])
  useEffect(() => {
    populateData()
  }, [])

  async function populateData() {
    const response = await fetch(`${BackendUrl}/api/books`);
    if (response.ok) {
      const data = await response.json();
      console.log(data);
      setBooks(data)

      // setForecasts(data);
    }
  }
  return (


    <>
      <div className="flex p-2 bg-slate-100">

        <div className="flex flex-1 justify-center gap-8">
          <Link href={'/'} className="p-2">Home</Link>
          <Link href={'/dashboard'} className="p-2">Dashboard</Link>
        </div>
        <div className="flex gap-4 px-4 py-2">
          <Link href={'/login'}>login</Link>
          <Link href={'/register'}>register</Link>
        </div>
      </div>

      <section className=" flex justify-center">

        <div className=" max-w-6xl flex flex-1 flex-col gap-4 p-4">
          <div className="grid auto-rows-min gap-4 md:grid-cols-4">
            {books.map((b, i) => (
              <div key={i} className="m-2 aspect-square   flex flex-col " >


                {/*  */}
                <div className="flex-1 bg-muted/50 rounded-xl">

                </div>

                {/*  */}
                <div className="pb-1"></div>



                {/*  */}
                <section className="">
                  <Drawer>
                    <DrawerTrigger>
                      <div className="">{b.bookName}</div>

                    </DrawerTrigger>
                    <DrawerContent >
                      <div className="flex justify">

                        <div className="h-[30rem] flex-1 aspect-squar rounded-lg m-1 bg-slate-300"></div>
                        <div className="max-w-4xl">

                          <DrawerHeader className="min-w-[25rem] h-full flex flex-col ">
                            <section className=" flex-1">

                              <DrawerTitle className="py-2">{b.bookName}</DrawerTitle>
                              
                              <DrawerDescription className="px-2">
                                <p >
                                  this is book Description.
                                </p>
                                <p className="text-wrap">................. .................. ................. ................ ................ </p>
                                <p className="text-wrap">................. .................. ................. ................ ................ </p>
                                <p className="text-wrap">................. .................. ................. ................ ................ </p>
                                <p className="text-wrap">................. .................. ................. ................ ................ </p>

                              </DrawerDescription>

                              <div className="p-2">

                                <div className="text-green-800">{b.genre}</div>
                                <div className="h-1"></div>
                                <div className="flex justify-aroun">

                                </div>
                              </div>
                            </section>
                            <Button>Buy
                              <span>
                                ${b.price}
                              </span>
                            </Button>
                          </DrawerHeader>

                        </div>
                      </div>
                    </DrawerContent>
                  </Drawer>

                  <div className="p-0.5"></div>
                  <div className="flex gap-4">

                    <div className="text-xs">{b.genre}</div>
                    <div className="text-xs">${b.price}</div>
                  </div>
                </section>


              </div>
            ))}
          </div>
        </div>

      </section>
    </>
  );
}
